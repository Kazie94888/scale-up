using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Users.GetLookup;

internal sealed record GetUserLookupQuery : IQuery<List<GetUserLookupResponseItem>>
{
    [FromQuery(Name = "searchTerm")]
    public string? SearchTerm { get; set; }
}