using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Delivery;

internal sealed class UpdateOrderDeliveryCommand : ICommand<UpdateOrderDeliveryResponse>
{
    [FromRoute(Name = "orderId")] 
    public required Guid OrderId { get; init; }

    [FromBody] 
    public required UpdateOrderDeliveryRequest Request { get; init; }
    public required UserInfoDto UserInfo { get; init; }
}

internal sealed class UpdateOrderDeliveryRequest
{
    [JsonPropertyName("address_line_1")]
    public required string AddressLine1 { get; init; }
    
    [JsonPropertyName("address_line_2")]
    public required string AddressLine2 { get; init; }
    
    [JsonPropertyName("contact_name")]
    public required string ContactName { get; init; }
    
    [JsonPropertyName("contact_phone")]
    public required string ContactPhone { get; init; }

    [JsonPropertyName("province")]
    public required string Province { get; init; }

    [JsonPropertyName("district")]
    public required string District { get; init; }

    [JsonPropertyName("ward")]
    public required string Ward { get; init; }

    [JsonPropertyName("province_code")]
    public required string ProvinceCode { get; init; }
    
    [JsonPropertyName("district_code")]
    public required string DistrictCode { get; init; }
    
    [JsonPropertyName("ward_code")]
    public required string WardCode { get; init; }
    
    [JsonPropertyName("tracking_code")]
    public required string TrackingCode { get; init; }
    
    [JsonPropertyName("tracking_company_code")]
    public required string TrackingCompanyCode { get; init; }
    
    [JsonPropertyName("tracking_url")]
    public required string TrackingUrl { get; init; }
    
    [JsonPropertyName("is_insurance")]
    public required bool IsInsurance { get; init; }
    
    [JsonPropertyName("package_weight")]
    public required decimal PackageWeight { get; init; }
    
    [JsonPropertyName("note_for_shipper")]
    public required string NoteForShipper { get; init; }
    
    [JsonPropertyName("delivery_method")]
    public required string DeliveryMethod { get; init; }
    
    [JsonPropertyName("insurance_price")]
    public required decimal InsurancePrice { get; init; }
    
    [JsonPropertyName("cod_amount_remain")]
    public required decimal CodAmountRemain { get; init; }
    
}