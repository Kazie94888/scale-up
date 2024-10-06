using System.Globalization;
using FluentResults;
using Microsoft.IdentityModel.Tokens;
using Refit;
using ScaleUp.Core.Application.Integrations.Haravan.Features.Orders;
using ScaleUp.Core.Application.Orders.Dtos;
using ScaleUp.Core.Application.Orders.Validations;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.SharedKernel.Configurations;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Entities;
using ScaleUp.Core.SharedKernel.Extensions;
using ScaleUp.Integrations.Haravan.ApiConsumers;
using ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;
using ScaleUp.Integrations.Haravan.Orders.Cancel;
using ScaleUp.Integrations.Haravan.Orders.Confirm;
using ScaleUp.Integrations.Haravan.Orders.Deliver;
using ScaleUp.Integrations.Haravan.Orders.Delivery;
using ScaleUp.Integrations.Haravan.Orders.Fulfillments.Update;
using ScaleUp.Integrations.Haravan.Orders.Payments;

namespace ScaleUp.Core.Application.Orders;

public class OrderManager(
    IHaravanOrderHubApi haravanOrderHubApi,
    IOrderActionValidator orderActionValidator,
    OrderCancelReasonConfigurations cancelReasonConfigs) : IOrderManager
{
    public async Task<Result> CancelOrder(Order order, string cancelReasonCode, string cancelNote, UserInfo cancelledBy)
    {
        var cancelReason = cancelReasonConfigs.Values.FirstOrDefault(x => x.Code.Equals(cancelReasonCode));
        if (cancelReason is null)
            return Result.Fail(ErrorConstants.InvalidCancelReason);

        var validationResponse = orderActionValidator.Validate(order, OrderAction.Cancel);
        if (!validationResponse.IsValid)
            return Result.Fail(validationResponse.ErrorMessages);

        var amount = order.FinancialStatus == OrderFinancialStatus.Paid.GetDescription() ? order.Total.ToString(CultureInfo.InvariantCulture) : "0";
        var haravanRequest = new CancelHaravanOrderRequest
        {
            OrderId = order.ExternalId,
            Note = cancelNote,
            Amount = amount,
            Reason = cancelReason.Code
        };

        var apiResponse = await haravanOrderHubApi.CancelOrder(haravanRequest);
        if (!apiResponse.IsSuccessStatusCode)
            return Result.Fail(ErrorConstants.FailedHaravanApi);

        order.Cancel(cancelReason.Code, cancelReason.Description, cancelNote, cancelledBy);

        return Result.Ok();
    }

    public async Task<Result> ConfirmOrder(Order order, string confirmedNote, UserInfo confirmedBy)
    {
        var haravanRequest = new ConfirmHaravanOrderRequest { OrderId = order.ExternalId, Note = confirmedNote };

        var validationResponse = orderActionValidator.Validate(order, OrderAction.Confirm);
        if (!validationResponse.IsValid)
            return Result.Fail(validationResponse.ErrorMessages);

        var apiResponse = await haravanOrderHubApi.ConfirmOrder(haravanRequest);
        if (!apiResponse.IsSuccessStatusCode)
            return Result.Fail(ErrorConstants.FailedHaravanApi);

        order.Confirm(confirmedNote, confirmedBy);

        return Result.Ok();
    }

    public Result PickOrder(Order order, string pickedNote, UserInfo pickedBy)
    {
        var validationResponse = orderActionValidator.Validate(order, OrderAction.Pick);
        if (!validationResponse.IsValid)
            return Result.Fail(validationResponse.ErrorMessages);

        order.Pick(pickedNote, pickedBy);

        return Result.Ok();
    }

    public Result PackOrder(Order order, string packedNote, UserInfo packedBy)
    {
        var validationResponse = orderActionValidator.Validate(order, OrderAction.Pack);
        if (!validationResponse.IsValid)
            return Result.Fail(validationResponse.ErrorMessages);

        order.Pack(packedNote, packedBy);

        return Result.Ok();
    }

    public async Task<Result> DeliverOrder(Order order, string deliveredNote, UserInfo deliveredBy)
    {
        var validationResponse = orderActionValidator.Validate(order, OrderAction.Deliver);
        if (!validationResponse.IsValid)
            return Result.Fail(validationResponse.ErrorMessages);

        var apiResponse = await haravanOrderHubApi.DeliverOrder(GetDeliverHaravanOrderRequest(order));
        if (!apiResponse.IsSuccessStatusCode)
            return Result.Fail(ErrorConstants.FailHaravanApi);

        var responseContent = apiResponse.Content;
        if (responseContent is null) return Result.Fail(ErrorConstants.InvalidFulfillment);

        order.AddFulfillment(HaravanOrderMapper.GetFulfillment(responseContent.Fulfillment));

        var trackingCompanyCode = HaravanOrderMapper.MapTrackingCode(responseContent.Fulfillment.TrackingCompanyCode,
            responseContent.Fulfillment.TrackingCompany);

        order.Delivery.Update(order.Code, HaravanOrderMapper.MapDeliveryMethod(trackingCompanyCode),
            responseContent.Fulfillment.TrackingNumber, trackingCompanyCode,
            responseContent.Fulfillment.TrackingUrl);

        order.Deliver(deliveredNote, deliveredBy);
        return Result.Ok();
    }

    public Result UpdateOrderStatus(Order order, string newStatus, string note, UserInfo updatedBy)
    {
        var orderStatus = OrderStatus.FromName(order.Status);
        var newOrderStatus = OrderStatus.FromName(newStatus);
        if (orderStatus.CanTransitionTo(newOrderStatus))
        {
            order.UpdateStatus(newStatus, note, updatedBy);

            return Result.Ok();
        }

        return Result.Fail(string.Format(ErrorConstants.CantChangeStatus, order.Status, newStatus));
    }

    public async Task<Result> UpdateDelivery(Order order, UpdateOrderDeliveryRequestDto request, UserInfo updatedBy)
    {
        var (contactName, contactPhone, addressLine1,
            addressLine2, ward, district,
            province, wardCode, districtCode,
            provinceCode, trackingCode, trackingCompanyCode,
            trackingUrl, isInsurance, packageWeight,
            noteForShipper, deliveryMethod, insurancePrice,
            codAmountRemain) = request;
        if (!order.Fulfillments.IsNullOrEmpty() && trackingCompanyCode.IsNotBlank() &&
            !order.Delivery.TrackingCompanyCode.EqualsTo(trackingCompanyCode))
        {
            var apiFulfillmentResponse = await UpdateHaravanOrderFulfillment(order, trackingCompanyCode);

            if (!apiFulfillmentResponse.IsSuccessStatusCode)
                return Result.Fail(ErrorConstants.FailUpdatedFulfillmentHaravanApi);
        }

        if (contactName.IsNotBlank() && contactPhone.IsNotBlank() && addressLine1.IsNotBlank() &&
            wardCode.IsNotBlank() && districtCode.IsNotBlank() && provinceCode.IsNotBlank())
        {
            var apiResponse = await UpdateHaravanOrderDelivery(order, contactName, contactPhone, addressLine1, wardCode,
                districtCode, provinceCode);

            if (!apiResponse.IsSuccessStatusCode)
                return Result.Fail(ErrorConstants.FailUpdatedDeliveryHaravanApi);

            order.Delivery.Update(contactName, contactPhone, addressLine1, addressLine2, ward, district, province,
                wardCode,
                districtCode, provinceCode, order.Delivery.TrackingCode ?? "", order.Delivery.TrackingCompanyCode ?? "",
                order.Delivery.TrackingUrl ?? "", order.Delivery.IsInsurance ?? false,
                order.Delivery.PackageWeight ?? 0,
                order.Delivery.NoteForShipper ?? "", order.Delivery.DeliveryMethod ?? "",
                order.Delivery.InsurancePrice ?? 0, order.Delivery.CodAmountRemain ?? 0);
        }
        else
        {
            if (codAmountRemain > order.Total)
                return Result.Fail(ErrorConstants.InvalidCodAmout);

            order.Delivery.Update(order.Delivery.ContactName!, order.Delivery.ContactPhone!,
                order.Delivery.AddressLine1!, order.Delivery.AddressLine2!,
                order.Delivery.Ward!, order.Delivery.District!, order.Delivery.Province!, order.Delivery.WardCode!,
                order.Delivery.DistrictCode!,
                order.Delivery.ProvinceCode!, trackingCode, trackingCompanyCode, trackingUrl, isInsurance,
                packageWeight,
                noteForShipper,
                deliveryMethod, insurancePrice, codAmountRemain);
        }

        order.UpdateDelivery(updatedBy);

        return Result.Ok();
    }

    private async Task<ApiResponse<HaravanFulfillment>> UpdateHaravanOrderFulfillment(Order order,
        string trackingCompanyCode)
    {
        var apiFulfillmentResponse = await haravanOrderHubApi.UpdateOrderFulfillment(
            new UpdateHaravanOrderFulfillmentRequest
            {
                OrderId = order.ExternalId,
                FulfillmentId = order.Fulfillments?.First().Id,
                FullfillmentRequest = new UpdateHaravanOrderFulfillmentRequest.Fullfillment
                {
                    TrackingNumber = trackingCompanyCode
                }
            });
        return apiFulfillmentResponse;
    }

    private async Task<ApiResponse<HaravanOrder>> UpdateHaravanOrderDelivery(Order order, string contactName,
        string contactPhone, string addressLine1, string wardCode, string districtCode, string provinceCode)
    {
        var names = GetName(contactName);

        var apiResponse = await haravanOrderHubApi.UpdateOrderDelivery(new UpdateHaravanOrderDeliveryRequest
        {
            OrderId = order.ExternalId,
            OrderRequest = new UpdateHaravanOrderDeliveryRequest.Order()
            {
                ShippingAddress = new UpdateHaravanOrderDeliveryRequest.ShippingAddress()
                {
                    Phone = contactPhone,
                    Address1 = addressLine1,
                    FirstName = names.Key,
                    LastName = names.Value,
                    WardCode = wardCode,
                    DistrictCode = districtCode,
                    ProvinceCode = provinceCode
                }
            }
        });
        return apiResponse;
    }

    public async Task<Result> ConfirmOrderPayment(Order order, UserInfo confirmedBy)
    {
        var haravanRequest = new ConfirmHaravanOrderPaymentRequest
        {
            Transaction = new Transaction
            {
                Amount = order.Total.ToString(CultureInfo.InvariantCulture),
                Kind = OrderPaymentKindTransaction.Capture.ToString(),
            },
            OrderId = order.ExternalId
        };

        var apiResponse = await haravanOrderHubApi.CreateOrderTransaction(haravanRequest);
        if (!apiResponse.IsSuccessStatusCode)
            return Result.Fail(ErrorConstants.FailedHaravanApi);


        order.ConfirmPayment(order.FinancialStatus!, order.Total, confirmedBy);

        return Result.Ok();
    }

    private DeliverHaravanOrderRequest GetDeliverHaravanOrderRequest(Order order)
    {
        var isInsurance = order.Delivery.IsInsurance ?? false;

        var haravanRequest = new DeliverHaravanOrderRequest
        {
            OrderId = order.ExternalId,
            Fulfillment = new FulfillmentRequest
            {
                RequestMode = DeliveredInformationConstants.Async,
                NotifyCustomer = false,
                TrackingCompany = order.Delivery.DeliveryMethod == DeliveredInformationConstants.CustomerSelfPick
                    ? string.Empty
                    : DeliveredInformationConstants.FastDelivery,
                LocationId = order.Warehouse.Code,
                NoteForShipper = order.Delivery.NoteForShipper,
                CarrierServicePackage = 2,
                CarrierServicePackageName = string.Empty,
                IsNewServicePackage = false,
                CarrierServiceCode = order.Delivery.DeliveryMethod == DeliveredInformationConstants.CustomerSelfPick
                    ? DeliveredInformationConstants.Other
                    : DeliveredInformationConstants.FastDeliveryCarrierCode,
                ReadToPickDate = DateTime.UtcNow,
                TotalWeight = order.Delivery.PackageWeight * 1000 ?? 0,
                CodAmount = order.Delivery.CodAmountRemain.GetValueOrDefault(),
                CarrierOptions = new CarrierOptions
                {
                    IsInsurance = isInsurance,
                    InsurancePrice = isInsurance ? order.Delivery.InsurancePrice ?? 0 : 0,
                    IsViewBefore = true,
                    IsOpenBox = true
                }
            }
        };

        return haravanRequest;
    }

    private static KeyValuePair<string, string> GetName(string name)
    {
        var names = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return new KeyValuePair<string, string>(
            names.Length > 2 ? name.Replace(names[0], string.Empty).Trim() : names.Length > 1 ? names[1] : names[0],
            names.Length > 1 ? names[0] : string.Empty);
    }
}