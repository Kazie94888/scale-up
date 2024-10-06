using MediatR;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.Domain.Events.Orders;
using ScaleUp.Core.Persistence.Context;

namespace ScaleUp.Core.Application.Orders.Pick;

internal class OrderPickedEventHandler(MasterDataContext dataContext) : INotificationHandler<OrderPickedEvent>
{
    public async Task Handle(OrderPickedEvent notification, CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.FirstAsync(x => x.Id == notification.OrderId, cancellationToken);

        var history = CreateOrderHistory(order, notification.PickedAt, notification.PreviousOrderStatus, notification.Note);
        order.AddHistory(history);

        dataContext.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    private static OrderHistory CreateOrderHistory(Order order, DateTime pickedAt, string previousStatus, string note)
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
            createdAt: pickedAt,
            createdBy: order.CreatedBy.Username,
            action: OrderAction.Pick,
            description: description,
            note: note
        );
    }
}