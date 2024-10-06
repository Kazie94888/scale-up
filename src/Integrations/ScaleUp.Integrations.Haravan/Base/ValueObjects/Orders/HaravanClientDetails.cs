using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanClientDetails
{
    [JsonPropertyName("accept_language")] public string AcceptLanguage { get; set; }
    [JsonPropertyName("browser_ip")] public string BrowserIp { get; set; }
    [JsonPropertyName("session_hash")] public string SessionHash { get; set; }
    [JsonPropertyName("user_agent")] public string UserAgent { get; set; }
    [JsonPropertyName("browser_height")] public decimal? BrowserHeight { get; set; }
    [JsonPropertyName("browser_width")] public decimal? BrowserWidth { get; set; }
}