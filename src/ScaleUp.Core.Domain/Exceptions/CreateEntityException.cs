namespace ScaleUp.Core.Domain.Exceptions;

public sealed class CreateEntityException(string? message = null) : DomainException(message);
