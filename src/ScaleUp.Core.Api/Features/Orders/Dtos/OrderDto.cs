using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos;


internal record OrderDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("assignee")]
    public AssigneeDto? Assignee { get; set; }

    [JsonPropertyName("assignee_id")]
    public Guid? AssigneeId { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("customer")]
    public OrderCustomerDto? Customer { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("total")]
    public required decimal Total { get; set; }

    [JsonPropertyName("channel")]
    public string? Channel { get; set; }

    [JsonPropertyName("warehouse")]
    public WarehouseDto? Warehouse { get; set; }
}
