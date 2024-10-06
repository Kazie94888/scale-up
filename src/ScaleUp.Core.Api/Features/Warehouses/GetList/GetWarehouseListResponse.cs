using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Warehouses.GetList;

internal sealed class GetWarehouseListResponse
{
    [JsonPropertyName("total")]
    public required int Total{ get; set; }
    
    [JsonPropertyName("data")]
    public IEnumerable<GetWarehouseListResponseItem>? Data { get; set; }
}

internal sealed class GetWarehouseListResponseItem
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}