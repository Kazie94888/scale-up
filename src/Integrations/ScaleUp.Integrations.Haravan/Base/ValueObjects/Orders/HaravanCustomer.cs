using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanCustomer
{
    [JsonPropertyName("accepts_marketing")]
    public bool? AcceptsMarketing { get; set; }

    [JsonPropertyName("addresses")] public List<HaravanAddress> Addresses { get; set; }
    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; }
    [JsonPropertyName("default_address")] public HaravanAddress DefaultAddress { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
    [JsonPropertyName("phone")] public string Phone { get; set; }
    [JsonPropertyName("first_name")] public string FirstName { get; set; }
    [JsonPropertyName("id")] public long? Id { get; set; }
    [JsonPropertyName("last_name")] public string LastName { get; set; }
    [JsonPropertyName("last_order_id")] public long? LastOrderId { get; set; }
    [JsonPropertyName("last_order_name")] public string LastOrderName { get; set; }
    [JsonPropertyName("note")] public string Note { get; set; }
    [JsonPropertyName("orders_count")] public long? OrdersCount { get; set; }
    [JsonPropertyName("state")] public string State { get; set; }
    [JsonPropertyName("tags")] public string Tags { get; set; }
    [JsonPropertyName("total_spent")] public decimal? TotalSpent { get; set; }
    [JsonPropertyName("updated_at")] public DateTime? UpdatedAt { get; set; }
    [JsonPropertyName("verified_email")] public bool? VerifiedEmail { get; set; }
    [JsonPropertyName("birthday")] public DateTime? Birthday { get; set; }
    [JsonPropertyName("gender")] public int? Gender { get; set; }
    [JsonPropertyName("last_order_date")] public DateTime? LastOrderDate { get; set; }

    [JsonPropertyName("multipass_identifier")]
    public string MultipassIdentifier { get; set; }
}