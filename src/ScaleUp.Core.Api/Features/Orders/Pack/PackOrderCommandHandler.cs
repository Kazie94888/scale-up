using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Application.Orders;
using ScaleUp.Core.Application.Orders.Validations;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Extensions;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Pack;

internal sealed class PackOrderCommandHandler(
    IOrderManager orderManager,
    MasterDataContext dataContext
) : ICommandHandler<PackOrderCommand, PackOrderResponse>
{
    public async Task<Result<PackOrderResponse>> Handle(PackOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.FirstAsync(x => x.Id == command.OrderId, cancellationToken);

        var packedResult = orderManager.PackOrder(order, command.Request.Note, command.UserInfo);
        if (packedResult.IsFailed)
            return packedResult;
        
        dataContext.Orders.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);
        
        return Result.Ok(new PackOrderResponse()
        {
            OrderId = order.Id
        });
    }
}