namespace ScaleUp.Core.Domain.Entities.Orders;

public sealed record OrderDelivery
{
    public string OrderCode { get; set; }
    public string? DeliveryMethod { get; set; }
    public string? ContactName { get; set; }
    public string? AddressLine1 { get; set; }
    public string? TrackingCode { get; set; }
    public string? TrackingCompanyCode { get; set; }
    public string? TrackingUrl { get; set; }
    public string? ContactPhone { get; set; }
    public string? AddressLine2 { get; set; }
    public string? Ward { get; set; }
    public string? District { get; set; }
    public string? Province { get; set; }
    public string? Country { get; set; }
    public decimal? PackageWeight { get; set; }
    public bool? IsInsurance { get; set; }
    public decimal? InsurancePrice { get; set; }
    public string? NoteForShipper { get; set; }
    public decimal? CodAmountRemain { get; set; }
    public string? DistrictCode { get; set; }
    public string? WardCode { get; set; }
    public string? ProvinceCode { get; set; }

    private OrderDelivery()
    {
    }

    public OrderDelivery(
        string orderCode,
        string? deliveryMethod,
        string? contactName,
        string? addressLine1,
        string? trackingCode,
        string? trackingCompanyCode,
        string? trackingUrl,
        string? contactPhone,
        string? addressLine2,
        string? ward,
        string? district,
        string? province,
        string? country,
        decimal? packageWeight,
        bool? isInsurance,
        decimal? insurancePrice,
        string? noteForShipper,
        decimal? codAmountRemain,
        string? wardCode,
        string? districtCode,
        string? provinceCode)
    {
        OrderCode = orderCode;
        DeliveryMethod = deliveryMethod;
        ContactName = contactName;
        AddressLine1 = addressLine1;
        TrackingCode = trackingCode;
        TrackingCompanyCode = trackingCompanyCode;
        TrackingUrl = trackingUrl;
        ContactPhone = contactPhone;
        AddressLine2 = addressLine2;
        Ward = ward;
        District = district;
        Province = province;
        Country = country;
        PackageWeight = packageWeight;
        IsInsurance = isInsurance;
        InsurancePrice = insurancePrice;
        NoteForShipper = noteForShipper;
        CodAmountRemain = codAmountRemain;
        WardCode = wardCode;
        DistrictCode = districtCode;
        ProvinceCode = provinceCode;
    }

    public void Update(string orderCode, string? deliveryMethod, string? trackingCode, string? trackingCompanyCode
        , string? trackingUrl)
    {
        OrderCode = orderCode;
        DeliveryMethod = deliveryMethod;
        TrackingCode = trackingCode;
        TrackingCompanyCode = trackingCompanyCode;
        TrackingUrl = trackingUrl;
    }

    public void Update(string contactName, string contactPhone, string addressLine1, string addressLine2,
        string ward, string district, string province, string wardCode, string districtCode, string provinceCode, string trackingCode, string trackingCompanyCode,
        string trackingUrl, bool isInsurance, decimal packageWeight, string noteForShipper, string deliveryMethod,
        decimal insurancePrice, decimal codAmountRemain)
    {
        ContactName = contactName;
        ContactPhone = contactPhone;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        Ward = ward;
        District = district;
        Province = province;
        WardCode = wardCode;
        DistrictCode = districtCode;
        ProvinceCode = provinceCode;
        TrackingCode = trackingCode;
        TrackingCompanyCode = trackingCompanyCode;
        TrackingUrl = trackingUrl;
        IsInsurance = isInsurance;
        PackageWeight = packageWeight;
        NoteForShipper = noteForShipper;
        InsurancePrice = insurancePrice;
        DeliveryMethod = deliveryMethod;
        CodAmountRemain = codAmountRemain;
    }
}