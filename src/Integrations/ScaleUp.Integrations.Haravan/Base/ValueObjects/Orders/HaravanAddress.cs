using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanAddress
{
    [JsonPropertyName("address1")] public string Address1 { get; set; }
    [JsonPropertyName("address2")] public string Address2 { get; set; }
    [JsonPropertyName("city")] public string City { get; set; }
    [JsonPropertyName("company")] public string Company { get; set; }
    [JsonPropertyName("country")] public string Country { get; set; }
    [JsonPropertyName("first_name")] public string FirstName { get; set; }
    [JsonPropertyName("id")] public long? Id { get; set; }
    [JsonPropertyName("last_name")] public string LastName { get; set; }
    [JsonPropertyName("phone")] public string Phone { get; set; }
    [JsonPropertyName("province")] public string Province { get; set; }
    [JsonPropertyName("zip")] public string Zip { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("province_code")] public string ProvinceCode { get; set; }
    [JsonPropertyName("country_code")] public string CountryCode { get; set; }
    [JsonPropertyName("default")] public bool? Default { get; set; }
    [JsonPropertyName("district")] public string District { get; set; }
    [JsonPropertyName("district_code")] public string DistrictCode { get; set; }
    [JsonPropertyName("ward")] public string Ward { get; set; }
    [JsonPropertyName("ward_code")] public string WardCode { get; set; }
}