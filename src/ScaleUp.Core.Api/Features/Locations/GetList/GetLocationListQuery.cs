using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Locations.GetList;

//TODO: Add Fluent Validation for Query
internal sealed record GetLocationListQuery : IQuery<List<LocationDto>>
{
    [FromQuery(Name = "parentId")]
    public Guid? ParentId { get; set; }

    [FromQuery(Name = "type")]
    public required string Type { get; set; }
}

public sealed record LocationDto
{
    public required Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public required string Type { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
}