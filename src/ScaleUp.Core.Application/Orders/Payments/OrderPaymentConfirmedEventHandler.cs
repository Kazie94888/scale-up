using MediatR;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.Domain.Events.Orders.Payments;
using ScaleUp.Core.Persistence.Context;

namespace ScaleUp.Core.Application.Orders.Payments;

internal sealed class OrderPaymentConfirmedEventHandler(MasterDataContext dataContext) : INotificationHandler<OrderPaymentConfirmedEvent>
{
    public async Task Handle(OrderPaymentConfirmedEvent notification, CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.FirstAsync(x => x.Id == notification.OrderId, cancellationToken);

        var history = CreateOrderHistory(order, notification.ConfirmedAt, notification.PreviousFinancialStatus);
        order.AddHistory(history);

        dataContext.Orders.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    private static OrderHistory CreateOrderHistory(Order order, DateTime confirmedAt, string previousFinancialStatus)
    {
        var description = new OrderHistoryDescription(
            previousStatus: previousFinancialStatus,
            assignee: null,
            usernameAssign: null,
            currentOrderDelivery: null,
            previousOrderDelivery: null,
            currentOrderWarehouse: null,
            currentPaymentMethod: null,
            previousPaymentMethod: null,
            cancelReasonCode: null
            );
        return new OrderHistory(orderCode: order.Code,
            orderStatus: order.Status,
            createdAt: confirmedAt,
            createdBy: order.CreatedBy.Username,
            action: OrderAction.ConfirmPayment,
            description: description,
            note: "");
    }
}