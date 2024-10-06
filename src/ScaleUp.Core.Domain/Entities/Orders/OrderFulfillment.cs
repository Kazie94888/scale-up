namespace ScaleUp.Core.Domain.Entities.Orders;

public sealed record OrderFulfillment
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
    public string? TrackingCompany { get; set; }
    public string? TrackingCompanyCode { get; set; }
    public string? TrackingNumber { get; set; }
    public string? TrackingUrl { get; set; }
    public DateTime? UpdateDateTime { get; set; }
    public List<OrderLineItem> LineItems { get; set; }
    public string? Province { get; set; }
    public string? District { get; set; }
    public string? Ward { get; set; }
    public decimal? CodAmount { get; set; }
    public string? CarrierStatusName { get; set; }
    public string? CarrierStatusCode { get; set; }
    public string? CarrierCodStatusName { get; set; }
    public string? CarrierCodStatusCode { get; set; }
    public string? LocationId { get; set; }
    public DateTime? ReadyToPickDate { get; set; }
    public DateTime? PickingDate { get; set; }
    public DateTime? DeliveredDate { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ShippingAddress { get; set; }
    public string? ShippingPhone { get; set; }

    private OrderFulfillment()
    {
    }

    public OrderFulfillment(
        string id,
        DateTime createdAt,
        string status,
        string? trackingCompany,
        string? trackingCompanyCode,
        string? trackingNumber,
        string? trackingUrl,
        DateTime? updateDateTime,
        List<OrderLineItem> lineItems,
        string? province,
        string? district,
        string? ward,
        decimal? codAmount,
        string? carrierStatusName,
        string? carrierStatusCode,
        string? carrierCodStatusName,
        string? carrierCodStatusCode,
        string locationId,
        DateTime? readyToPickDate,
        DateTime? pickingDate,
        DateTime? deliveredDate,
        string? firstName,
        string? lastName,
        string? shippingAddress,
        string? shippingPhone)
    {
        Id = id;
        CreatedAt = createdAt;
        Status = status;
        TrackingCompany = trackingCompany;
        TrackingCompanyCode = trackingCompanyCode;
        TrackingNumber = trackingNumber;
        TrackingUrl = trackingUrl;
        UpdateDateTime = updateDateTime;
        LineItems = lineItems;
        Province = province;
        District = district;
        Ward = ward;
        CodAmount = codAmount;
        CarrierStatusName = carrierStatusName;
        CarrierStatusCode = carrierStatusCode;
        CarrierCodStatusName = carrierCodStatusName;
        CarrierCodStatusCode = carrierCodStatusCode;
        LocationId = locationId;
        ReadyToPickDate = readyToPickDate;
        PickingDate = pickingDate;
        DeliveredDate = deliveredDate;
        FirstName = firstName;
        LastName = lastName;
        ShippingAddress = shippingAddress;
        ShippingPhone = shippingPhone;
    }
}