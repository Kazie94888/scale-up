using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Shared.Dtos;

internal sealed record LocationDto
{
    public LocationDto(string? name, string? code)
    {
        Name = name;
        Code = code;
    }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

}
