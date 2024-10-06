using System.Text.Json.Serialization;
using Refit;

namespace ScaleUp.Integrations.Haravan.Orders.Deliver;

public class DeliverHaravanOrderRequest
{
    [AliasAs("orderId")]
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }
    
    
    [JsonPropertyName("fulfillment")]
    public FulfillmentRequest Fulfillment { get; set; }
}

public class FulfillmentRequest
{
    [JsonPropertyName("request_mode")]
    public string RequestMode { get; set; }

    [JsonPropertyName("notify_customer")]
    public bool NotifyCustomer { get; set; }
    
    [JsonPropertyName("tracking_company")]
    public string TrackingCompany { get; set; }
    
    [JsonPropertyName("location_id")]
    public string LocationId { get; set; }
    
    [JsonPropertyName("note")]
    public string NoteForShipper { get; set; }
    
    [JsonPropertyName("carrier_service_package")]
    public int CarrierServicePackage { get; set; }
    
    [JsonPropertyName("carrier_service_package_name")]
    public string CarrierServicePackageName { get; set; } 
    
    [JsonPropertyName("is_new_service_package")]
    public bool IsNewServicePackage { get; set; }
    
    [JsonPropertyName("carrier_service_code")]
    public string CarrierServiceCode { get; set; }
    
    [JsonPropertyName("ready_to_pick_date")]
    public DateTime ReadToPickDate { get; set; }
    
    [JsonPropertyName("total_weight")]
    public decimal TotalWeight { get; set; }
    
    [JsonPropertyName("cod_amount")]
    public decimal CodAmount { get; set; }
    
    [JsonPropertyName("carrier_options")]
    public CarrierOptions CarrierOptions { get; set; }
}

public class CarrierOptions
{
    [JsonPropertyName("is_insurance")]
    public bool IsInsurance { get; set; }
    
    [JsonPropertyName("insurance_price")]
    public decimal InsurancePrice { get; set; }
    
    [JsonPropertyName("is_view_before")]
    public bool IsViewBefore { get; set; }
    
    [JsonPropertyName("is_open_box")]
    public bool IsOpenBox { get; set; }
}