using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos;

internal sealed record OrderHistoryDto
{
    [JsonPropertyName("order_code")]
    public string? OrderCode { get; set; }


    [JsonPropertyName("order_status")]
    public string? OrderStatus { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("action")]
    public string? Action { get; set; }

    [JsonPropertyName("description")]
    public OrderHistoryDescriptionDto? Description { get; set; }

    [JsonPropertyName("note")]
    public string? Note { get; set; }
}

internal sealed record OrderHistoryDescriptionDto
{
    [JsonPropertyName("previous_status")]
    public string? PreviousStatus { get; set; }

    [JsonPropertyName("assignee")]
    public AssigneeDto? Assignee { get; set; }

    [JsonPropertyName("usernameAssign")]
    public string? UsernameAssign { get; set; }

    [JsonPropertyName("current_order_delivery")]
    public string? CurrentOrderDelivery { get; set; }

    [JsonPropertyName("previous_order_delivery")]
    public string? PreviousOrdeDelivery { get; set; }

    [JsonPropertyName("current_order_warehouse")]
    public string? CurrentOrderWarehouse { get; set; }

    [JsonPropertyName("current_payment_method")]
    public string? CurrentPaymentMethod { get; set; }

    [JsonPropertyName("previous_payment_method")]
    public string? PreviousPaymentMethod { get; set; }

    [JsonPropertyName("cancel_reason_code")]
    public string? CancelReasonCode { get; set; }

}