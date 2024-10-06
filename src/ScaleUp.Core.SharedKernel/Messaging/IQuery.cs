using FluentResults;
using MediatR;

namespace ScaleUp.Core.SharedKernel.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
