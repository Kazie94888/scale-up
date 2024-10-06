using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos;


internal sealed record OrderDetailsDto : OrderDto
{
    [JsonPropertyName("note")]
    public string? Note { get; set; }

    [JsonPropertyName("delivery")]
    public OrderDeliveryDto? Delivery { get; set; }

    [JsonPropertyName("financial_status")]
    public string? FinancialStatus { get; set; }

    [JsonPropertyName("discount_amount")]
    public decimal? DiscountAmount { get; set; }

    [JsonPropertyName("ffm_id")]
    public int? FulfillmentId { get; set; }

    [JsonPropertyName("lines")]
    public required IEnumerable<OrderLineDto> Lines { get; set; }

    [JsonPropertyName("payment")]
    public IEnumerable<OrderPaymentDto>? Payment { get; set; }

    [JsonPropertyName("shipping_fee")]
    public decimal? ShippingFee { get; set; }

    [JsonPropertyName("subtotal")]
    public decimal? SubTotal { get; set; }

    [JsonPropertyName("external_code")]
    public string? ExternalCode { get; set; }

    [JsonPropertyName("external_created_at")]
    public DateTime? ExternalCreatedAt { get; set; }

    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("external_updated_at")]
    public DateTime? ExternalUpdatedAt { get; set; }

    [JsonPropertyName("group_relative_id")]
    public Guid? GroupRelativeId { get; set; }

    [JsonPropertyName("relative_orders")]
    public IEnumerable<RelativeOrderDto>? RelativeOrders { get; set; }

    [JsonPropertyName("history")]
    public IEnumerable<OrderHistoryDto>? History { get; set; }

    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; set; }
}
