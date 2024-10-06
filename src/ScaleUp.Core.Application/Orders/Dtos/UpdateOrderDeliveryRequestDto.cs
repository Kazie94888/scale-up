namespace ScaleUp.Core.Application.Orders.Dtos;

public sealed class UpdateOrderDeliveryRequestDto
{
    public required string AddressLine1 { get; init; }
    
    public required string AddressLine2 { get; init; }
    
    public required string ContactName { get; init; }
    
    public required string ContactPhone { get; init; }

    public required string Province { get; init; }

    public required string District { get; init; }

    public required string Ward { get; init; }

    public required string ProvinceCode { get; init; }
    
    public required string DistrictCode { get; init; }
    
    public required string WardCode { get; init; }
    
    public required string TrackingCode { get; init; }
    
    public required string TrackingCompanyCode { get; init; }
    
    public required string TrackingUrl { get; init; }
    
    public required bool IsInsurance { get; init; }
    
    public required decimal PackageWeight { get; init; }
    
    public required string NoteForShipper { get; init; }
    
    public required string DeliveryMethod { get; init; }
    
    public required decimal InsurancePrice { get; init; }
    
    public required decimal CodAmountRemain { get; init; }

    public void Deconstruct(
        out string contactName, out string contactPhone, out string addressLine1,
        out string addressLine2, out string ward, out string district, 
        out string province, out string wardCode, out string districtCode,
        out string provinceCode, out string trackingCode, out string trackingCompanyCode, 
        out string trackingUrl, out bool isInsurance, out decimal packageWeight, 
        out string noteForShipper,out string deliveryMethod, out decimal insurancePrice, 
        out decimal codAmountRemain)
    {
        contactName = ContactName;
        contactPhone = ContactPhone;
        addressLine1 = AddressLine1;
        addressLine2 = AddressLine2;
        ward = Ward;
        district = District;
        province = Province;
        wardCode = WardCode;
        districtCode = DistrictCode;
        provinceCode = ProvinceCode;
        trackingCode = TrackingCode;
        trackingCompanyCode = TrackingCompanyCode;
        trackingUrl = TrackingUrl;
        isInsurance = IsInsurance;
        packageWeight = PackageWeight;
        noteForShipper = NoteForShipper;
        deliveryMethod = DeliveryMethod;
        insurancePrice = InsurancePrice;
        codAmountRemain = CodAmountRemain;
    }
}