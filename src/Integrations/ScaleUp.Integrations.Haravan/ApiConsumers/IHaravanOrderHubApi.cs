using Refit;
using ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;
using ScaleUp.Integrations.Haravan.Orders.Cancel;
using ScaleUp.Integrations.Haravan.Orders.Confirm;
using ScaleUp.Integrations.Haravan.Orders.Deliver;
using ScaleUp.Integrations.Haravan.Orders.Delivery;
using ScaleUp.Integrations.Haravan.Orders.Fulfillments.Update;
using ScaleUp.Integrations.Haravan.Orders.Get;
using ScaleUp.Integrations.Haravan.Orders.GetList;
using ScaleUp.Integrations.Haravan.Orders.Payments;

namespace ScaleUp.Integrations.Haravan.ApiConsumers;

public interface IHaravanOrderHubApi
{
    [Get("/com/orders.json?page={page}&limit={limit}&created_at_min={created_at_min}&created_at_max={created_at_max}")]
    Task<GetHaravanOrdersResponse> GetOrders(
        [AliasAs("page")] int page,
        [AliasAs("limit")] int limit,
        [AliasAs("created_at_min")] DateTime? createdAtMin,
        [AliasAs("created_at_max")] DateTime? createdAtMax);

    [Get("/com/orders/{orderId}.json")]
    Task<ApiResponse<GetHaravanOrderResponse>> GetOrder(string orderId);

    [Post("/com/orders/{request.orderId}/cancel.json")]
    Task<ApiResponse<HaravanOrder>> CancelOrder([Body] CancelHaravanOrderRequest request);

    [Post("/com/orders/{request.orderId}/confirm.json")]
    Task<ApiResponse<HaravanOrder>> ConfirmOrder([Body] ConfirmHaravanOrderRequest request);

    [Post("/com/orders/{request.orderId}/transactions.json")]
    Task<ApiResponse<HaravanTransaction>> CreateOrderTransaction([Body] ConfirmHaravanOrderPaymentRequest request);

    [Post("/com/orders/{request.orderId}/fulfillments.json")]
    Task<ApiResponse<DeliverHaravanOrderResponse>> DeliverOrder([Body] DeliverHaravanOrderRequest request);

    [Put("/com/orders/{request.orderId}.json")]
    Task<ApiResponse<HaravanOrder>> UpdateOrderDelivery([Body] UpdateHaravanOrderDeliveryRequest request);

    [Put("/com/orders/{request.orderId}/fulfillments/{request.fulfillmentId}.json")]
    Task<ApiResponse<HaravanFulfillment>> UpdateOrderFulfillment([Body] UpdateHaravanOrderFulfillmentRequest request);
}