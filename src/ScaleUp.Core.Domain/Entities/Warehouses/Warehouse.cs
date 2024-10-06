using MongoDB.Bson.Serialization.Attributes;
using ScaleUp.Core.Domain.Base.Interfaces;
using ScaleUp.Core.SharedKernel.Entities;

namespace ScaleUp.Core.Domain.Entities.Warehouses;

public sealed class Warehouse : AggregateRoot, IMultiTenant
{
    [BsonElement("_id")]
    public required Guid Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? LocationType { get; set; }
    public string? Email { get; set; }
    public required string Address1 { get; set; }
    public string? Address2 { get; set; }
    public required string Zip { get; set; }
    public string? City { get; set; }
    public required string Province { get; set; }
    public required string Country { get; set; }
    public required string Phone { get; set; }
    public required string CountryCode { get; set; }
    public required string CountryName { get; set; }
    public string? ProvinceCode { get; set; }
    public required string District { get; set; }
    public string? DistrictCode { get; set; }
    public required string Ward { get; set; }
    public string? WardCode { get; set; }
    public Guid TenantId { get; set; }
}