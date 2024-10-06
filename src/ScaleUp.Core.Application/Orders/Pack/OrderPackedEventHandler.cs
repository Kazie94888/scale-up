using MediatR;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.Domain.Events.Orders;
using ScaleUp.Core.Persistence.Context;

namespace ScaleUp.Core.Application.Orders.Pack;

internal sealed class OrderPackedEventHandler(MasterDataContext dataContext) : INotificationHandler<OrderPackedEvent>
{
    public async Task Handle(OrderPackedEvent notification, CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.FirstAsync(x => x.Id == notification.OrderId, cancellationToken);

        var history = CreateOrderHistory(order, notification.PackedAt, notification.PreviousOrderStatus,
            notification.Note);
        order.AddHistory(history);

        dataContext.Orders.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    private static OrderHistory CreateOrderHistory(Order order, DateTime packedAt, string previousStatus, string note)
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
            cancelReasonCode: null
            );
        return new OrderHistory(
            orderCode: order.Code,
            orderStatus: order.Status,
            createdAt: packedAt,
            createdBy: order.CreatedBy.Username,
            action: OrderAction.Pack,
            description: description,
            note: note
            );
    }
}