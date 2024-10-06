using MediatR;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.Domain.Events.Orders;
using ScaleUp.Core.Persistence.Context;

namespace ScaleUp.Core.Application.Orders.Sync;

internal sealed class OrderSyncedEventHandler(MasterDataContext dataContext) : INotificationHandler<OrderSyncedEvent>
{
    public async Task Handle(OrderSyncedEvent notification, CancellationToken cancellationToken)
    {
        var order = notification.Order;

        if (notification.ConfirmedAt.HasValue)
            order.AddHistory(CreateOrderConfirmedHistory(order, notification.ConfirmedAt, notification.ConfirmedStatus));

        if (notification.CancelledAt.HasValue)
            order.AddHistory(CreateOrderCancelledHistory(order, notification.CancelledAt, notification.CancelReason));

        if (notification.ClosedAt.HasValue)
            order.AddHistory(CreateOrderClosedHistory(order, notification.ClosedAt, notification.ClosedStatus));


        dataContext.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    private OrderHistory CreateOrderClosedHistory(Order order, DateTime? closedAt, string? closedStatus)
    {
        return new OrderHistory(
            orderCode: order.Code,
            orderStatus: closedStatus,
            createdAt: closedAt!.Value,
            createdBy: order.CreatedBy.Username,
            action: OrderAction.Close,
            description: null,
            note: "Order closed"
        );
    }

    private OrderHistory CreateOrderCancelledHistory(Order order, DateTime? cancelledAt, string? cancelReason)
    {
        var description = new OrderHistoryDescription(
            previousStatus: null,
            assignee: null,
            usernameAssign: null,
            currentOrderDelivery: null,
            previousOrderDelivery: null,
            currentOrderWarehouse: null,
            currentPaymentMethod: null,
            previousPaymentMethod: null,
            cancelReasonCode: cancelReason
        );

        return new OrderHistory(
            orderCode: order.Code,
            orderStatus: OrderStatus.Cancelled,
            createdAt: cancelledAt!.Value,
            createdBy: order.CreatedBy.Username,
            action: OrderAction.Cancel,
            description: description,
            note: "Order cancelled"
        );
    }

    private OrderHistory CreateOrderConfirmedHistory(Order order, DateTime? confirmedAt, string? confirmedStatus)
    {
        return new OrderHistory(
            orderCode: order.Code,
            orderStatus: confirmedStatus,
            createdAt: confirmedAt!.Value,
            createdBy: order.CreatedBy.Username,
            action: OrderAction.Confirm,
            description: null,
            note: "Order confirmed"
        );
    }
}