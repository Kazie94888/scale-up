using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Shared.Dtos
{
    internal sealed record VariantDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
