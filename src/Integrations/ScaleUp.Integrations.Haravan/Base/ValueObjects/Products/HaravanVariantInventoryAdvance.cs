using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Products;

public sealed record HaravanVariantInventoryAdvance
{                    
    [JsonPropertyName("qty_available")]
    public int QtyAvailable { get; set; }
    [JsonPropertyName("qty_onhand")]
    public int QtyOnhand { get; set; }
    [JsonPropertyName("qty_commited")]
    public int QtyCommited { get; set; }
    [JsonPropertyName("qty_incoming")]
    public int QtyIncoming { get; set; }
}