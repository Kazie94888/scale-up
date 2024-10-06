using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Entities.Warehouses;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.Domain.Shared;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Entities;
using ScaleUp.Core.SharedKernel.Extensions;
using ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

namespace ScaleUp.Core.Application.Integrations.Haravan.Features.Orders;

public static class HaravanOrderMapper
{
    private const string _fulfilled = "fulfilled";
    private const string _delivered = "delivered";
    private const string _delivering = "delivering";
    private const string _return = "return";
    private const string _waitingForReturn = "waiting_for_return";
    private const string _notMeetCustomer = "notmeetcustomer";
    private const string _pending = "pending";
    private const string _picking = "picking";
    private const string _readyToPick = "readytopick";
    private const string _codPaid = "codpaid";
    private const string _none = "none";
    private const string _paid = "paid";
    private const string _close = "close";
    private const string _cancel = "cancel";

    public static Order Map(HaravanOrder haravanOrder, Warehouse? warehouse, Guid tenantId)
    {
        var customer = GetCustomer(haravanOrder);
        var lineItems = GetLineItems(haravanOrder.LineItems, haravanOrder.OrderNumber);
        var delivery = GetDelivery(haravanOrder);
        var payments = GetPayments(haravanOrder);
        var orderWarehouse = GetWarehouse(haravanOrder, warehouse);
        var status = GetStatus(haravanOrder);
        var fulfillments = GetFulfillments(haravanOrder);

        var order = Order.Create(
            haravanOrder.OrderNumber,
            haravanOrder.OrderId?.ToString() ?? string.Empty,
            haravanOrder.OrderNumber,
            haravanOrder.SourceName,
            OrderSourceType.Haravan.ToString(),
            status,
            customer,
            lineItems,
            haravanOrder.SubtotalPrice ?? 0,
            haravanOrder.TotalDiscounts ?? 0,
            haravanOrder.FinancialStatus,
            haravanOrder.ShippingLines.Sum(line => line.Price ?? 0),
            0, // TODO update field: This needs to be calculated or fetched from another source
            haravanOrder.TotalTax ?? 0,
            haravanOrder.TotalPrice ?? 0 - haravanOrder.TotalTax ?? 0,
            haravanOrder.TotalPrice ?? 0,
            "TODO update field",
            null,
            delivery,
            payments,
            new OrderCancelReason("TODO update field", haravanOrder.CancelReason),
            orderWarehouse,
            fulfillments,
            UserInfo.Integration,
            haravanOrder.CreatedAt.GetValueOrDefault(),
            tenantId
        );

        order.Sync(haravanOrder.ConfirmedAt, haravanOrder.ConfirmedStatus, haravanOrder.CancelledAt,
            haravanOrder.CancelReason, haravanOrder.ClosedAt, haravanOrder.ClosedStatus, order.CreatedBy);

        return order;
    }

    private static OrderCustomer GetCustomer(HaravanOrder haravanOrder)
    {
        return new OrderCustomer(haravanOrder.Customer.FirstName, haravanOrder.Customer.LastName,
            haravanOrder.Customer.Email, haravanOrder.Customer.Phone);
    }

    private static OrderDelivery GetDelivery(HaravanOrder haravanOrder)
    {
        var fulfillment = haravanOrder.Fulfillments.FirstOrDefault(); //Todo: handle many Fulfillments
        var trackingCompanyCode = MapTrackingCode(fulfillment?.TrackingCompanyCode, fulfillment?.TrackingCompany);
        var delivery = new OrderDelivery(
            haravanOrder.OrderNumber,
            MapDeliveryMethod(trackingCompanyCode),
            trackingCode: fulfillment?.TrackingNumber,
            trackingCompanyCode: trackingCompanyCode,
            trackingUrl: fulfillment?.TrackingUrl,
            contactName: haravanOrder.ShippingAddress.Name,
            contactPhone: haravanOrder.ShippingAddress.Phone,
            addressLine1: haravanOrder.ShippingAddress.Address1,
            addressLine2: haravanOrder.ShippingAddress.Address2,
            ward: haravanOrder.ShippingAddress.Ward,
            district: haravanOrder.ShippingAddress.District,
            province: haravanOrder.ShippingAddress.Province,
            country: haravanOrder.ShippingAddress.Country,
            packageWeight: haravanOrder.TotalWeight,
            isInsurance: fulfillment?.IsInsurance,
            insurancePrice: fulfillment?.InsurancePrice,
            noteForShipper: fulfillment?.Note,
            codAmountRemain: haravanOrder.FinancialStatus == OrderFinancialStatus.Paid.GetDescription()
                ? 0
                : GetCodAmount(haravanOrder),
            wardCode: haravanOrder.ShippingAddress.WardCode,
            districtCode: haravanOrder.ShippingAddress.DistrictCode,
            provinceCode: haravanOrder.ShippingAddress.ProvinceCode
        );
        return delivery;
    }

    private static List<OrderPayment> GetPayments(HaravanOrder haravanOrder)
    {
        return haravanOrder.Transactions.Select(transaction => new OrderPayment
        (
            haravanOrder.OrderNumber,
            transaction.Id.ToString(),
            transaction.Amount,
            haravanOrder.GatewayCode,
            haravanOrder.Gateway,
            haravanOrder.FinancialStatus.EqualsTo(OrderFinancialStatus.Paid.GetDescription())
                ? OrderFinancialStatus.Paid.GetDescription()
                : OrderFinancialStatus.Unpaid.GetDescription(),
            haravanOrder.BillingAddress.Address1,
            haravanOrder.BillingAddress.Address2,
            haravanOrder.BillingAddress.Ward,
            haravanOrder.BillingAddress.District,
            haravanOrder.BillingAddress.City,
            haravanOrder.BillingAddress.Country,
            transaction.CreatedAt
        )).ToList();
    }

    private static string GetStatus(HaravanOrder haravanOrder)
    {
        var fulfillments = haravanOrder.Fulfillments;
        var fs = haravanOrder.FinancialStatus;
        var ffms = haravanOrder.FulfillmentStatus;
        var (csc, ccsc) = fulfillments.Count > 0
            ? (fulfillments[0].CarrierStatusCode, fulfillments[0].CarrierCodStatusCode)
            : (null, null);

        if (ffms.EqualsTo(_fulfilled))
        {
            if (csc.EqualsTo(_delivered) && (ccsc.HasIndexIn(_codPaid, _none) || fs.EqualsTo(_paid)))
                return OrderStatus.Completed;

            if (csc.EqualsTo(_return)) return OrderStatus.Returned;

            if (csc.EqualsTo(_waitingForReturn)) return OrderStatus.WaitingForReturn;

            if (csc.EqualsTo(_delivered)) return OrderStatus.Delivered;

            if (csc.HasIndexIn(_delivering, _pending, _notMeetCustomer)) return OrderStatus.Delivering;

            if (csc.EqualsTo(_picking)) return OrderStatus.DeliveryPicking;

            if (csc.EqualsTo(_readyToPick)) return OrderStatus.Packed;
        }

        if (haravanOrder.OrderProcessingStatus.EqualsTo(_close)) return OrderStatus.Completed;

        if (haravanOrder.OrderProcessingStatus.EqualsTo(_cancel)) return OrderStatus.Cancelled;

        if (haravanOrder.OrderProcessingStatus.EqualsTo(_pending)) return OrderStatus.New;

        return haravanOrder.OrderProcessingStatus;
    }

    private static OrderWarehouse GetWarehouse(HaravanOrder haravanOrder, Warehouse? warehouse)
    {
        return new OrderWarehouse(
            Code: haravanOrder.LocationId?.ToString() ?? warehouse!.Code,
            Name: haravanOrder.LocationName,
            Source: OrderSourceType.Haravan.ToString(),
            AddressLine1: warehouse!.Address1,
            AddressLine2: warehouse.Address2,
            Ward: warehouse.Ward,
            District: warehouse.District,
            Province: warehouse.City,
            Country: warehouse.Country,
            WardCode: warehouse.WardCode,
            DistrictCode: warehouse.DistrictCode,
            ProvinceCode: warehouse.ProvinceCode
        );
    }

    private static List<OrderFulfillment> GetFulfillments(HaravanOrder haravanOrder)
    {
        return haravanOrder.Fulfillments.Select(GetFulfillment).ToList();
    }

    public static OrderFulfillment GetFulfillment(HaravanFulfillment haravanFulfillment)
    {
        return new OrderFulfillment(
            haravanFulfillment.Id.ToString() ?? string.Empty,
            haravanFulfillment.CreatedAt ?? DateTime.UtcNow,
            haravanFulfillment.Status,
            haravanFulfillment.TrackingCompany,
            haravanFulfillment.TrackingCompanyCode,
            haravanFulfillment.TrackingNumber,
            haravanFulfillment.TrackingUrl,
            haravanFulfillment.UpdatedAt,
            GetLineItems(haravanFulfillment.LineItems, haravanFulfillment.OrderId.ToString() ?? string.Empty),
            haravanFulfillment.Province,
            haravanFulfillment.District,
            haravanFulfillment.Ward,
            haravanFulfillment.CodAmount.GetValueOrDefault(),
            haravanFulfillment.CarrierStatusName,
            haravanFulfillment.CarrierStatusCode,
            haravanFulfillment.CarrierCodStatusName,
            haravanFulfillment.CarrierCodStatusCode,
            haravanFulfillment.LocationId.ToString() ?? string.Empty,
            haravanFulfillment.ReadyToPickDate ?? DateTime.UtcNow,
            haravanFulfillment.PickingDate,
            haravanFulfillment.DeliveredDate,
            haravanFulfillment.FirstName,
            haravanFulfillment.LastName,
            haravanFulfillment.ShippingAddress,
            haravanFulfillment.ShippingPhone);
    }

    private static List<OrderLineItem> GetLineItems(List<HaravanOrderLineItem> haravanOrderLineItems, string orderCode)
    {
        return haravanOrderLineItems.Select(haravanItem => new OrderLineItem(
                orderCode,
                haravanItem.Sku,
                haravanItem.Barcode,
                haravanItem.Id?.ToString() ?? "TODO update field",
                haravanItem.Title,
                "TODO update field: category",
                haravanItem.PriceOriginal,
                haravanItem.PricePromotion,
                haravanItem.PriceOriginal.GetValueOrDefault(),
                haravanItem.TotalDiscount,
                haravanItem.Quantity.GetValueOrDefault(),
                haravanItem.Properties?.Select(property => new Variant(property.Name, property.Value)).ToList(),
                haravanItem.Price, "TODO update field: RefPromotion",
                haravanItem.ProductId.ToString(), // TODO: Map to SU product ID to get Product details from SU
                "TODO update field: ImageUrl"))
            .ToList();
    }

    public static string? MapTrackingCode(string? trackingCompanyCode, string? trackingCompany)
    {
        if (trackingCompanyCode.IsBlank())
            trackingCompanyCode = trackingCompany switch
            {
                DeliveredInformationConstants.FastDelivery => DeliveredInformationConstants.FastDeliveryCarrierCode,
                DeliveredInformationConstants.ShopeeXpress => DeliveredInformationConstants.ShopeeXpressCode,
                _ => DeliveredInformationConstants.Other
            };
        return trackingCompanyCode;
    }

    public static string? MapDeliveryMethod(string? trackingCompanyCode)
    {
        return trackingCompanyCode switch
        {
            DeliveredInformationConstants.FastDeliveryCarrierCode => DeliveredInformationConstants.Fast,
            _ => DeliveredInformationConstants.CustomerSelfPick
        };
    }

    private static decimal GetCodAmount(HaravanOrder haravanOrder)
    {
        if (haravanOrder.Transactions.Count > 0)
            return haravanOrder.Transactions.Select(t => t.Amount).Sum();
        return haravanOrder.TotalPrice ?? 0;
    }
}