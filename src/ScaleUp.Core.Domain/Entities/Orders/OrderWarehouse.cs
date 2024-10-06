namespace ScaleUp.Core.Domain.Entities.Orders;

public sealed record OrderWarehouse(
    string Code,
    string Name,
    string? Source,
    string? AddressLine1,
    string? AddressLine2,
    string? Ward,
    string? District,
    string? Province,
    string? Country,
    string? WardCode,
    string? DistrictCode,
    string? ProvinceCode);