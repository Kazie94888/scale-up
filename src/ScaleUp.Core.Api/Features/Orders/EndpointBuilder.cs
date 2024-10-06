using MediatR;
using ScaleUp.Core.Api.Base;
using ScaleUp.Core.Api.Base.Extensions;
using ScaleUp.Core.Api.Features.Orders.Cancel;
using ScaleUp.Core.Api.Features.Orders.CancelReasons.GetList;
using ScaleUp.Core.Api.Features.Orders.Confirm;
using ScaleUp.Core.Api.Features.Orders.Deliver;
using ScaleUp.Core.Api.Features.Orders.Delivery;
using ScaleUp.Core.Api.Features.Orders.Get;
using ScaleUp.Core.Api.Features.Orders.GetList;
using ScaleUp.Core.Api.Features.Orders.Pack;
using ScaleUp.Core.Api.Features.Orders.Payments.Confirm;
using ScaleUp.Core.Api.Features.Orders.Pick;
using ScaleUp.Core.Api.Features.Orders.Status.GetList;
using ScaleUp.Core.Api.Features.Orders.Status.Update;

namespace ScaleUp.Core.Api.Features.Orders
{
    internal class EndpointBuilder : IEndpointBuilder
    {
        public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
        {
            #region Orders

            routeBuilder.MapGet("/orders", async (
                IMediator mediator,
                [AsParameters] GetOrderListQuery query,
                CancellationToken cancellationToken
            ) =>
            {
                var result = await mediator.Send(query, cancellationToken);
                return result.ToHttpResult();
            });
            //.RequireAuthorization(); //un-comment this line to use authorization

            routeBuilder.MapGet("/orders/{orderId}", async (
                IMediator mediator,
                [AsParameters] GetOrderDetailsQuery query,
                CancellationToken cancellationToken
            ) =>
            {
                var result = await mediator.Send(query, cancellationToken);
                return result.ToHttpResult();
            });

            routeBuilder.MapPut("/orders/{orderId}/cancel", async (
               IMediator mediator,
               [AsParameters] CancelOrderCommand command,
               CancellationToken cancellationToken
            ) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.ToHttpResult();
            });

            routeBuilder.MapPut("/orders/{orderId}/confirm", async (
                IMediator mediator,
                [AsParameters] ConfirmOrderCommand command,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.ToHttpResult();
            });

            routeBuilder.MapPut("/orders/{orderId}/pack", async (
                IMediator mediator,
                [AsParameters] PackOrderCommand command,
                CancellationToken cancellationToken
            ) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.ToHttpResult();
            });


            routeBuilder.MapPut("/orders/{orderId}/pick", async (
                IMediator mediator,
                [AsParameters] PickOrderCommand command,
                CancellationToken cancellationToken
            ) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.ToHttpResult();
            });

            routeBuilder.MapPut("/orders/{orderId}/deliver", async (
                IMediator mediator,
                [AsParameters] DeliverOrderCommand command,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.ToHttpResult();
            });
            #endregion

            #region Order Status

            routeBuilder.MapGet("/orders/status", async (
                IMediator mediator,
               [AsParameters] GetOrderStatusListQuery query,
               CancellationToken cancellationToken
            ) =>
            {
                var result = await mediator.Send(query, cancellationToken);
                return result.ToHttpResult();
            });

            routeBuilder.MapPut("/orders/{orderId}/status", async (
                IMediator mediator,
                [AsParameters] UpdateOrderStatusCommand command,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.ToHttpResult();
            });

            #endregion

            #region Order Payment

            routeBuilder.MapPut("/orders/{orderId}/payments/confirm", async (
                IMediator mediator,
                [AsParameters] ConfirmOrderPaymentCommand command,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.ToHttpResult();
            });

            #endregion

            #region Order Cancel Reason

            routeBuilder.MapGet("/orders/cancel-reasons", async (
               IMediator mediator,
               [AsParameters] GetOrderCancelReasonListQuery query,
               CancellationToken cancellationToken
            ) =>
            {
                var result = await mediator.Send(query, cancellationToken);
                return result.ToHttpResult();
            });

            #endregion

            #region Order Delivery
            routeBuilder.MapPut("/orders/{orderId}/delivery", async (
                IMediator mediator,
                [AsParameters] UpdateOrderDeliveryCommand command,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.ToHttpResult();
            });
            #endregion
        }
    }
}
