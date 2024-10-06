using ScaleUp.Core.Domain.Enums;

namespace ScaleUp.Core.Domain.Entities.Products;

public sealed record ExternalId(PlatformType Source, string Id);