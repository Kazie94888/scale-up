using System.Text.Json.Serialization;
using Refit;

namespace ScaleUp.Integrations.Haravan.Orders.Delivery;

public sealed class UpdateHaravanOrderDeliveryRequest
{
    [AliasAs("orderId")]
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }
    
    [JsonPropertyName("order")]
    public Order OrderRequest { get; set; }
    
    public sealed class Order
    {
        [JsonPropertyName("shipping_address")]
        public ShippingAddress ShippingAddress { get; set; }
    }

    public sealed class ShippingAddress
    {
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    
        [JsonPropertyName("address1")]
        public string Address1 { get; set; }
    
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
    
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
    
        [JsonPropertyName("ward_code")]
        public string WardCode { get; set; }
    
        [JsonPropertyName("district_code")]
        public string DistrictCode { get; set; }
    
        [JsonPropertyName("province_code")]
        public string ProvinceCode { get; set; }

    }
}