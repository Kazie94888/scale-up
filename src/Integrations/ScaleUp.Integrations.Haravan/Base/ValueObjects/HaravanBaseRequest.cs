using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects;

public record HaravanBaseRequest
{
    [JsonPropertyName("page")]
    public int Page { get; set; } = HaravanSyncConstants.Page;
    [JsonPropertyName("limit")]
    public int Limit { get; set; } = HaravanSyncConstants.PageSize;
    [JsonPropertyName("created_at_min")]
    public required DateTime CreatedAtMin { get; set; }
    [JsonPropertyName("created_at_max")]
    public required DateTime CreatedAtMax { get; set; }
}