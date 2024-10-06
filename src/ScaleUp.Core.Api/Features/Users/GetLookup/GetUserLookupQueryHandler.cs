using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Extensions;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Users.GetLookup;

internal sealed class GetUserLookupQueryHandler(ReadOnlyMasterDataContext dataContext) : IQueryHandler<GetUserLookupQuery, List<GetUserLookupResponseItem>>
{
    public async Task<Result<List<GetUserLookupResponseItem>>> Handle(GetUserLookupQuery request, CancellationToken cancellationToken)
    {
        var userQuery = dataContext.Users;

        if (request.SearchTerm.IsNotBlank())
        {
            var searchTerm = request.SearchTerm!.Trim();
            userQuery = userQuery.Where(u => u.FirstName.Contains(searchTerm) || u.LastName.Contains(searchTerm) || u.Email.Contains(searchTerm));
        }

        var users = await userQuery.ToListAsync(cancellationToken);
        var result = users.Select(u => new GetUserLookupResponseItem
        {
            Id = u.Id,
            Name = u.FullName,
            Email = u.Email,
            Status = u.Status
        }).ToList();

        return Result.Ok(result);
    }
}