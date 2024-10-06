using ScaleUp.Core.Api.Shared.Dtos;
using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Dtos;

internal sealed record OrderDeliveryDto
{

    [JsonPropertyName("tracking_url")]
    public string? TrackingUrl { get; set; }

    [JsonPropertyName("tracking_code")]
    public string? TrackingCode { get; set; }

    [JsonPropertyName("tracking_company_code")]
    public string? TrackingCompanyCode { get; set; }

    [JsonPropertyName("contact_name")]
    public string? ContactName { get; set; }

    [JsonPropertyName("contact_phone")]
    public string? ContactPhone { get; set; }

    [JsonPropertyName("note_for_shipper")]
    public string? NoteForShipper { get; set; }

    [JsonPropertyName("is_insurance")]
    public bool? IsInsurance { get; set; }

    [JsonPropertyName("package_weight")]
    public decimal? PackageWeight { get; set; }

    [JsonPropertyName("delivery_method")]
    public string? DeliveryMethod { get; set; }

    [JsonPropertyName("insurance_price")]
    public decimal? InsurancePrice { get; set; }

    [JsonPropertyName("cod_amount_remain")]
    public decimal? CodAmountRemain { get; set; }

    [JsonPropertyName("address_line_1")]
    public string? AddressLine1 { get; set; }

    [JsonPropertyName("address_line_2")]
    public string? AddressLine2 { get; set; }

    [JsonPropertyName("ward")]
    public LocationDto? Ward { get; set; }

    [JsonPropertyName("district")]
    public LocationDto? District { get; set; }

    [JsonPropertyName("province")]
    public LocationDto? Province { get; set; }
}
