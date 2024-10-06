using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanFulfillment
{
    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; }
    [JsonPropertyName("id")] public long? Id { get; set; }
    [JsonPropertyName("order_id")] public long? OrderId { get; set; }
    [JsonPropertyName("receipt")] public string Receipt { get; set; }
    [JsonPropertyName("status")] public string Status { get; set; }
    [JsonPropertyName("tracking_company")] public string TrackingCompany { get; set; }
    [JsonPropertyName("tracking_company_code")] public string TrackingCompanyCode { get; set; }
    [JsonPropertyName("tracking_numbers")] public List<string> TrackingNumbers { get; set; }
    [JsonPropertyName("tracking_number")] public string TrackingNumber { get; set; }
    [JsonPropertyName("tracking_url")] public string TrackingUrl { get; set; }
    [JsonPropertyName("tracking_urls")] public List<string> TrackingUrls { get; set; }
    [JsonPropertyName("updated_at")] public DateTime? UpdatedAt { get; set; }
    [JsonPropertyName("line_items")] public List<HaravanOrderLineItem> LineItems { get; set; }
    [JsonPropertyName("province")] public string Province { get; set; }
    [JsonPropertyName("province_code")] public string ProvinceCode { get; set; }
    [JsonPropertyName("district")] public string District { get; set; }
    [JsonPropertyName("district_code")] public string DistrictCode { get; set; }
    [JsonPropertyName("ward")] public string Ward { get; set; }
    [JsonPropertyName("ward_code")] public string WardCode { get; set; }
    [JsonPropertyName("cod_amount")] public decimal? CodAmount { get; set; }

    [JsonPropertyName("carrier_status_name")]
    public string CarrierStatusName { get; set; }

    [JsonPropertyName("carrier_cod_status_name")]
    public string CarrierCodStatusName { get; set; }

    [JsonPropertyName("carrier_status_code")]
    public string CarrierStatusCode { get; set; }

    [JsonPropertyName("carrier_cod_status_code")]
    public string CarrierCodStatusCode { get; set; }

    [JsonPropertyName("location_id")] public long? LocationId { get; set; }
    [JsonPropertyName("location_name")] public string LocationName { get; set; }
    [JsonPropertyName("note")] public string Note { get; set; }

    [JsonPropertyName("carrier_service_package_name")]
    public string CarrierServicePackageName { get; set; }

    [JsonPropertyName("coupon_code")] public string CouponCode { get; set; }

    [JsonPropertyName("ready_to_pick_date")]
    public DateTime? ReadyToPickDate { get; set; }

    [JsonPropertyName("picking_date")] public DateTime? PickingDate { get; set; }
    [JsonPropertyName("delivering_date")] public DateTime? DeliveringDate { get; set; }
    [JsonPropertyName("delivered_date")] public DateTime? DeliveredDate { get; set; }
    [JsonPropertyName("return_date")] public DateTime? ReturnDate { get; set; }

    [JsonPropertyName("not_meet_customer_date")]
    public DateTime? NotMeetCustomerDate { get; set; }

    [JsonPropertyName("waiting_for_return_date")]
    public DateTime? WaitingForReturnDate { get; set; }

    [JsonPropertyName("cod_paid_date")] public DateTime? CodPaidDate { get; set; }
    [JsonPropertyName("cod_receipt_date")] public DateTime? CodReceiptDate { get; set; }
    [JsonPropertyName("cod_pending_date")] public DateTime? CodPendingDate { get; set; }

    [JsonPropertyName("cod_not_receipt_date")]
    public DateTime? CodNotReceiptDate { get; set; }

    [JsonPropertyName("cancel_date")] public DateTime? CancelDate { get; set; }
    [JsonPropertyName("is_view_before")] public bool? IsViewBefore { get; set; }
    [JsonPropertyName("country")] public string Country { get; set; }
    [JsonPropertyName("country_code")] public string CountryCode { get; set; }
    [JsonPropertyName("zip_code")] public string ZipCode { get; set; }
    [JsonPropertyName("city")] public string City { get; set; }

    [JsonPropertyName("real_shipping_fee")]
    public decimal? RealShippingFee { get; set; }

    [JsonPropertyName("shipping_notes")] public string ShippingNotes { get; set; }
    [JsonPropertyName("total_weight")] public decimal? TotalWeight { get; set; }
    [JsonPropertyName("package_length")] public decimal? PackageLength { get; set; }
    [JsonPropertyName("package_width")] public decimal? PackageWidth { get; set; }
    [JsonPropertyName("package_height")] public decimal? PackageHeight { get; set; }

    [JsonPropertyName("boxme_servicecode")]
    public string BoxmeServicecode { get; set; }

    [JsonPropertyName("transport_type")] public int? TransportType { get; set; }
    [JsonPropertyName("address")] public string Address { get; set; }
    [JsonPropertyName("sender_phone")] public string SenderPhone { get; set; }
    [JsonPropertyName("sender_name")] public string SenderName { get; set; }

    [JsonPropertyName("carrier_service_code")]
    public string CarrierServiceCode { get; set; }

    [JsonPropertyName("from_longtitude")] public decimal? FromLongtitude { get; set; }
    [JsonPropertyName("from_latitude")] public decimal? FromLatitude { get; set; }
    [JsonPropertyName("to_longtitude")] public decimal? ToLongtitude { get; set; }
    [JsonPropertyName("to_latitude")] public decimal? ToLatitude { get; set; }
    [JsonPropertyName("sort_code")] public string SortCode { get; set; }
    [JsonPropertyName("is_drop_off")] public bool? IsDropOff { get; set; }
    [JsonPropertyName("is_insurance")] public bool? IsInsurance { get; set; }
    [JsonPropertyName("insurance_price")] public decimal? InsurancePrice { get; set; }
    [JsonPropertyName("is_open_box")] public bool? IsOpenBox { get; set; }
    [JsonPropertyName("request_id")] public string RequestId { get; set; }
    [JsonPropertyName("carrier_options")] public string CarrierOptions { get; set; }
    [JsonPropertyName("note_attributes")] public List<HaravanAttribute> NoteAttributes { get; set; }
    [JsonPropertyName("first_name")] public string FirstName { get; set; }
    [JsonPropertyName("last_name")] public string LastName { get; set; }
    [JsonPropertyName("shipping_address")] public string ShippingAddress { get; set; }
    [JsonPropertyName("shipping_phone")] public string ShippingPhone { get; set; }
}

public sealed record HaravanAttribute
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("value")]
    public string Value { get; set; }
}