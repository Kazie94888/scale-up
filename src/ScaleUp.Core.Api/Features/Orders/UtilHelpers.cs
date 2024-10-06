

using ScaleUp.Core.Api.Features.Orders.Dtos;
using ScaleUp.Core.Api.Shared.Dtos;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.SharedKernel.Extensions;

namespace ScaleUp.Core.Api.Features.Orders;

internal static class UtilHelpers
{
    public static WarehouseDto GetWarehouse(Order order)
    {
        return new WarehouseDto
        {
            Name = order.Warehouse.Name,
            Code = order.Warehouse.Code,
            Source = order.Warehouse.Source,
            AddressLine1 = order.Warehouse.AddressLine1,
            Ward = order.Delivery.Ward.IsBlank() ? null : new LocationDto(order.Delivery.Ward, order.Delivery.WardCode),
            District = order.Delivery.District.IsBlank() ? null : new LocationDto(order.Delivery.District, order.Delivery.DistrictCode),
            Province = order.Delivery.Province.IsBlank() ? null : new LocationDto(order.Delivery.Province, order.Delivery.ProvinceCode),
        };
    }
    
    public static IEnumerable<OrderPaymentDto> GetPayment(Order order)
    {
        return order.Payments.Select(x => new OrderPaymentDto
        {
            Amount = x.Amount,
            PaymentStatus = x.PaymentStatus ?? PaymentStatus.Unpaid.ToString(),
            PaymentCode = x.PaymentCode,
            PaymentMethod = x.PaymentMethod,
            PaymentGateway = x.PaymentGateway,
            CreatedAt = x.CreatedAt,
        });
    }

    public static AssigneeDto? GetAssignee(Order order)
    {
        return order.Assignee is null ? null : new AssigneeDto
        {
            Id = order.Assignee.Id,
            Name = order.Assignee.FullName
        };
    }

    public static OrderCustomerDto GetCustomer(Order order)
    {
        return new OrderCustomerDto
        {
            Profiles =
                 [
                     new()
                    {
                      FirstName=    order.Customer.FirstName,
                      LastName=  order.Customer.LastName,
                      Email = order.Customer.Email,
                      PhoneNumber =  order.Customer.PhoneNumber
                    }
                 ]
        };
    }

    public static OrderDeliveryDto GetDelivery(Order order)
    {
        return new OrderDeliveryDto
        {
            TrackingUrl = order.Delivery.TrackingUrl,
            TrackingCode = order.Delivery.TrackingCode,
            TrackingCompanyCode = order.Delivery.TrackingCompanyCode,
            ContactName = order.Delivery.ContactName,
            ContactPhone = order.Delivery.ContactPhone,
            NoteForShipper = order.Delivery.NoteForShipper,
            IsInsurance = order.Delivery.IsInsurance.GetValueOrDefault(),
            PackageWeight = order.Delivery.PackageWeight.GetValueOrDefault(),
            DeliveryMethod = order.Delivery.DeliveryMethod,
            InsurancePrice = order.Delivery.InsurancePrice,
            CodAmountRemain = order.Delivery.CodAmountRemain,
            AddressLine1 = order.Delivery.AddressLine1,
            AddressLine2 = order.Delivery.AddressLine2,
            Ward = order.Delivery.Ward.IsBlank() ? null : new LocationDto(order.Delivery.Ward, order.Delivery.WardCode),
            District = order.Delivery.District.IsBlank() ? null : new LocationDto(order.Delivery.District, order.Delivery.DistrictCode),
            Province = order.Delivery.Province.IsBlank() ? null : new LocationDto(order.Delivery.Province, order.Delivery.ProvinceCode),
        };
    }

    public static IEnumerable<OrderLineDto> GetOrderLines(Order order)
    {
        return order.Lines.Select(l => new OrderLineDto
        {
            ItemCode = l.ItemCode,
            ItemName = l.ItemName,
            Quantity = l.Quantity,
            Sku = l.Sku,
            LineTotal = l.LineTotal,
            ListPrice = l.ListPrice,
            Price = l.Price,
            DiscountAmount = l.DiscountAmount,
            ImageUrl = l.ImageUrl,
            Variants = l.Variants?.Select(v => new VariantDto
            {
                Name = v.Name,
                Value = v.Value
            })
        });
    }

    public static IEnumerable<OrderHistoryDto> GetHistory(Order order)
    {
        return order.History.OrderByDescending(x => x.CreatedAt).Select(x => new OrderHistoryDto
        {
            OrderCode = x.OrderCode,
            Action = x.Action,
            CreatedAt = x.CreatedAt,
            Note = x.Note,
            OrderStatus = x.OrderStatus,
            CreatedBy = x.CreatedBy,
            Description = x.Description is null ? null : new OrderHistoryDescriptionDto
            {
                CancelReasonCode = x.Description.CancelReasonCode,
                CurrentOrderDelivery = x.Description.CurrentOrderDelivery,
                PreviousStatus = x.Description.PreviousStatus,
                UsernameAssign = x.Description.UsernameAssign,
                CurrentOrderWarehouse = x.Description.CurrentOrderWarehouse,
                CurrentPaymentMethod = x.Description.CurrentPaymentMethod,
                PreviousOrdeDelivery = x.Description.PreviousOrderDelivery,
                PreviousPaymentMethod = x.Description.PreviousPaymentMethod,
                Assignee = x.Description.Assignee is null ? null : new AssigneeDto
                {
                    Id = x.Description.Assignee.Id,
                    Name = x.Description?.Assignee.FullName
                }
            }
        });

    }
}