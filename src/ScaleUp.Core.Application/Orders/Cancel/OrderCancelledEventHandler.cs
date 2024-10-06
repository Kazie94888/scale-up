using MediatR;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.Domain.Events.Orders;
using ScaleUp.Core.Persistence.Context;

namespace ScaleUp.Core.Application.Orders.Cancel;

internal sealed class OrderCancelledEventHandler(MasterDataContext dataContext) : INotificationHandler<OrderCancelledEvent>
{
    public async Task Handle(OrderCancelledEvent notification, CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.FirstOrDefaultAsync(x => x.Id == notification.OrderId, cancellationToken);
        if (order != null)
        {
            var history = CreateOrderHistory(order, notification.CancelledAt, notification.PreviousOrderStatus, notification.Note);
            order.AddHistory(history);

            dataContext.Orders.Update(order);
            await dataContext.SaveChangesAsync(cancellationToken);
        }
    }

    private static OrderHistory CreateOrderHistory(Order order, DateTime cancelledAt, string previousStatus, string note)
    {
        var description = new OrderHistoryDescription(
            previousStatus: previousStatus,
            assignee: null,
            usernameAssign: null,
            currentOrderDelivery: null,
            previousOrderDelivery: null,
            currentOrderWarehouse: null,
            currentPaymentMethod: null,
            previousPaymentMethod: null,
            cancelReasonCode: order.CancelReasonCode
        );

        return new OrderHistory(
            orderCode: order.Code,
            orderStatus: order.Status,
            createdAt: cancelledAt,
            createdBy: order.CreatedBy.Username,
            action: OrderAction.Cancel,
            description: description,
            note: note
        );
    }
}
