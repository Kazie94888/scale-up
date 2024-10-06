using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Extensions;
using ScaleUp.Core.SharedKernel.Models;

namespace ScaleUp.Core.Application.Orders.Validations;

public interface IOrderActionValidator
{
    ValidationResponse Validate(Order order, OrderAction orderAction);
}

public sealed class OrderActionValidator : IOrderActionValidator
{
    public ValidationResponse Validate(Order order, OrderAction orderAction)
    {
        var validationResponse = new ValidationResponse();

        var orderStatus = OrderStatus.FromName(order.Status);
        if (!orderStatus.CanDoAction(orderAction))
        {
            validationResponse.AddError(string.Format(orderAction switch
            {
                _ when orderAction == OrderAction.Cancel => ErrorConstants.CantCancel,
                _ when orderAction == OrderAction.Confirm => ErrorConstants.CantConfirm,
                _ when orderAction == OrderAction.Pick => ErrorConstants.CantPick,
                _ when orderAction == OrderAction.Pack => ErrorConstants.CantPack,
                _ when orderAction == OrderAction.Deliver => ErrorConstants.CantDeliver,
                _ => string.Empty
            }, order.Status));
        }


        if (orderAction == OrderAction.Confirm)
        {
            if (!ValidatePayment(order.Payments))
                validationResponse.AddError(ErrorConstants.InvalidPayment);

            if (!ValidateCustomer(order.Customer))
                validationResponse.AddError(ErrorConstants.InvalidCustomer);
        }

        if (orderAction == OrderAction.Deliver)
            if (!ValidateDelivery(order.Delivery))
                validationResponse.AddError(ErrorConstants.InvalidDelivery);

        return validationResponse;
    }

    private static bool ValidatePayment(List<OrderPayment>? orderPayments)
    {
        return orderPayments is not null && orderPayments.Any(x => x.PaymentMethod.HasIndexIn(
            PaymentMethod.Cod.GetDescription(),
            PaymentStatus.Paid.GetDescription(),
            PaymentStatus.BankDeposit.GetDescription()));
    }

    private static bool ValidateCustomer(OrderCustomer customer)
    {
        return customer.FirstName.IsNotBlank() && customer.PhoneNumber.IsNotBlank();
    }

    private static bool ValidateDelivery(OrderDelivery delivery)
    {
        return delivery.ContactPhone.IsNotBlank()
               && (delivery.AddressLine1.IsNotBlank() ||
                   delivery.AddressLine2.IsNotBlank())
               && delivery.Ward.IsNotBlank()
               && delivery.District.IsNotBlank()
               && delivery.Province.IsNotBlank();
    }
}