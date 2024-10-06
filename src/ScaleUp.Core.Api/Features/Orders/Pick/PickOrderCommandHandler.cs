using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Application.Orders;
using ScaleUp.Core.Application.Orders.Validations;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Extensions;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Pick;

internal sealed class PickOrderCommandHandler(
    IOrderManager orderManager,
    MasterDataContext dataContext) : ICommandHandler<PickOrderCommand, PickOrderResponse>
{
    public async Task<Result<PickOrderResponse>> Handle(PickOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.FirstAsync(x => x.Id == command.OrderId, cancellationToken: cancellationToken);

        var pickedResult = orderManager.PickOrder(order, command.Request.Note, command.UserInfo);
        if (pickedResult.IsFailed)
            return pickedResult;
        
        dataContext.Orders.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);
        
        return Result.Ok(new PickOrderResponse()
        {
            OrderId = order.Id
        });
    }
}