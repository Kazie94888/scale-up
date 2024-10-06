using MediatR;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Customers;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.Domain.Events.Orders;
using ScaleUp.Core.Persistence.Context;

namespace ScaleUp.Core.Application.Orders.Create;

internal sealed class OrderCreatedEventHandler(MasterDataContext dataContext) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var order = notification.Order;

        var history = CreateOrderHistory(order);
        order.AddHistory(history);

        await UpdateCustomer(order, cancellationToken);

        dataContext.Orders.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateCustomer(Order order, CancellationToken cancellationToken)
    {
        var orderCustomer = order.Customer;
        var customer = await dataContext.Customers.FirstOrDefaultAsync(x => x.PhoneNumber == orderCustomer.PhoneNumber, cancellationToken);//TODO: check phone number in different format
        if (customer is null)
        {
            await dataContext.Customers.AddAsync(
                Customer.Create(orderCustomer.FirstName!,
                    orderCustomer.LastName!,
                    orderCustomer.PhoneNumber!,
                    orderCustomer.Email!,
                    order.CreatedBy), cancellationToken);
        }
        else
        {
            customer.UpdateCustomerInfo(orderCustomer.FirstName!, orderCustomer.LastName!, orderCustomer.Email!);
            dataContext.Customers.Update(customer);
        }
    }

    private static OrderHistory CreateOrderHistory(Order order)
    {
        var description = new OrderHistoryDescription(
            previousStatus: null,
            assignee: order.Assignee,
            usernameAssign: order.Assignee?.FullName,
            currentOrderDelivery: null,
            previousOrderDelivery: null,
            currentOrderWarehouse: order.Warehouse?.Name,
            currentPaymentMethod: null,
            previousPaymentMethod: null,
            cancelReasonCode: null
        );

        return new OrderHistory(
            orderCode: order.Code,
            orderStatus: order.Status,
            createdAt: order.CreatedAt,
            createdBy: order.CreatedBy.Username,
            action: OrderAction.Create,
            description: description,
            note: $"Order created on SU from {order.Source}"
        );
    }
}
