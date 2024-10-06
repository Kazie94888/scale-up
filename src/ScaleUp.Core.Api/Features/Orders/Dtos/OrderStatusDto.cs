using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos;

internal sealed record OrderStatusDto
{
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("label_type")]
    public string? LabelType { get; set; }

    [JsonPropertyName("allowed_statuses")]
    public required List<string> AllowedStatuses { get; init; }

    [JsonPropertyName("allowed_actions")]
    public required List<string> AllowedActions { get; init; }
}