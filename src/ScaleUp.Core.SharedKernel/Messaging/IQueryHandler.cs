using FluentResults;
using MediatR;

namespace ScaleUp.Core.SharedKernel.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
