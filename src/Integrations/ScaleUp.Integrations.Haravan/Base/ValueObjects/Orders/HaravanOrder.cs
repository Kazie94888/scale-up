using System.Text.Json.Serialization;

namespace ScaleUp.Integrations.Haravan.Base.ValueObjects.Orders;

public sealed record HaravanOrder
{
    // json props from documents
    [JsonPropertyName("billing_address")] public HaravanAddress BillingAddress { get; set; }
    [JsonPropertyName("browser_ip")] public string BrowserIp { get; set; }

    [JsonPropertyName("buyer_accepts_marketing")]
    public bool? BuyerAcceptsMarketing { get; set; }

    [JsonPropertyName("cancel_reason")] public string CancelReason { get; set; }
    [JsonPropertyName("cancelled_at")] public DateTime? CancelledAt { get; set; }
    [JsonPropertyName("cart_token")] public string CartToken { get; set; }
    [JsonPropertyName("checkout_token")] public string CheckoutToken { get; set; }
    [JsonPropertyName("client_details")] public HaravanClientDetails ClientDetails { get; set; }
    [JsonPropertyName("closed_at")] public DateTime? ClosedAt { get; set; }
    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; }
    [JsonPropertyName("currency")] public string Currency { get; set; }
    [JsonPropertyName("customer")] public HaravanCustomer Customer { get; set; }
    [JsonPropertyName("discount_codes")] public List<HaravanDiscountCode> DiscountCodes { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
    [JsonPropertyName("financial_status")] public string FinancialStatus { get; set; }
    [JsonPropertyName("fulfillments")] public List<HaravanFulfillment> Fulfillments { get; set; }

    [JsonPropertyName("fulfillment_status")] public string FulfillmentStatus { get; set; }

    [JsonPropertyName("tags")] public string Tags { get; set; }
    [JsonPropertyName("gateway")] public string Gateway { get; set; }
    [JsonPropertyName("gateway_code")] public string GatewayCode { get; set; }
    [JsonPropertyName("id")] public long? OrderId { get; set; }
    [JsonPropertyName("landing_site")] public string LandingSite { get; set; }
    [JsonPropertyName("landing_site_ref")] public string LandingSiteRef { get; set; }
    [JsonPropertyName("source")] public string Source { get; set; }
    [JsonPropertyName("line_items")] public List<HaravanOrderLineItem> LineItems { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("note")] public string Note { get; set; }
    [JsonPropertyName("number")] public int? Number { get; set; }
    [JsonPropertyName("order_number")] public string OrderNumber { get; set; }

    [JsonPropertyName("processing_method")] public string ProcessingMethod { get; set; }

    [JsonPropertyName("referring_site")] public string ReferringSite { get; set; }
    [JsonPropertyName("refunds")] public List<HaravanRefund> Refunds { get; set; }
    [JsonPropertyName("shipping_address")] public HaravanAddress ShippingAddress { get; set; }
    [JsonPropertyName("shipping_lines")] public List<HaravanShippingLine> ShippingLines { get; set; }
    [JsonPropertyName("source_name")] public string SourceName { get; set; }
    [JsonPropertyName("subtotal_price")] public decimal? SubtotalPrice { get; set; }
    [JsonPropertyName("tax_lines")] public string TaxLines { get; set; }
    [JsonPropertyName("taxes_included")] public bool? TaxesIncluded { get; set; }
    [JsonPropertyName("token")] public string Token { get; set; }
    [JsonPropertyName("total_discounts")] public decimal? TotalDiscounts { get; set; }

    [JsonPropertyName("total_line_items_price")] public decimal? TotalLineItemsPrice { get; set; }

    [JsonPropertyName("total_price")] public decimal? TotalPrice { get; set; }
    [JsonPropertyName("total_tax")] public decimal? TotalTax { get; set; }
    [JsonPropertyName("total_weight")] public decimal? TotalWeight { get; set; }
    [JsonPropertyName("updated_at")] public DateTime? UpdatedAt { get; set; }
    [JsonPropertyName("transactions")] public List<HaravanTransaction> Transactions { get; set; }
    [JsonPropertyName("note_attributes")] public List<HaravanNoteAttribute> NoteAttributes { get; set; }
    [JsonPropertyName("confirmed_at")] public DateTime? ConfirmedAt { get; set; }
    [JsonPropertyName("closed_status")] public string ClosedStatus { get; set; }
    [JsonPropertyName("cancelled_status")] public string CancelledStatus { get; set; }
    [JsonPropertyName("confirmed_status")] public string ConfirmedStatus { get; set; }

    [JsonPropertyName("assigned_location_id")]
    public long? AssignedLocationId { get; set; }

    [JsonPropertyName("assigned_location_name")]
    public string AssignedLocationName { get; set; }

    [JsonPropertyName("assigned_location_at")]
    public DateTime? AssignedLocationAt { get; set; }

    [JsonPropertyName("exported_confirm_at")]
    public DateTime? ExportedConfirmAt { get; set; }

    [JsonPropertyName("user_id")] public long? UserId { get; set; }
    [JsonPropertyName("device_id")] public long? DeviceId { get; set; }
    [JsonPropertyName("location_id")] public long? LocationId { get; set; }
    [JsonPropertyName("location_name")] public string LocationName { get; set; }
    [JsonPropertyName("ref_order_id")] public long? RefOrderId { get; set; }
    [JsonPropertyName("ref_order_date")] public DateTime? RefOrderDate { get; set; }
    [JsonPropertyName("ref_order_number")] public string RefOrderNumber { get; set; }
    [JsonPropertyName("utm_source")] public string UtmSource { get; set; }
    [JsonPropertyName("utm_medium")] public string UtmMedium { get; set; }
    [JsonPropertyName("utm_campaign")] public string UtmCampaign { get; set; }
    [JsonPropertyName("utm_term")] public string UtmTerm { get; set; }
    [JsonPropertyName("utm_content")] public string UtmContent { get; set; }
    [JsonPropertyName("payment_url")] public string PaymentUrl { get; set; }
    [JsonPropertyName("contact_email")] public string ContactEmail { get; set; }

    [JsonPropertyName("order_processing_status")]
    public string OrderProcessingStatus { get; set; }

    [JsonPropertyName("prev_order_id")] public long? PrevOrderId { get; set; }

    [JsonPropertyName("prev_order_number")]
    public string PrevOrderNumber { get; set; }

    [JsonPropertyName("prev_order_date")] public DateTime? PrevOrderDate { get; set; }
    [JsonPropertyName("redeem_model")] public HaravanRedeemModel RedeemModel { get; set; }
    [JsonPropertyName("confirm_user")] public decimal? ConfirmUser { get; set; }
    [JsonPropertyName("risk_level")] public string RiskLevel { get; set; }

    [JsonPropertyName("discount_applications")]
    public string DiscountApplications { get; set; }
}