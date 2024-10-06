namespace ScaleUp.Core.Domain.Exceptions;

public sealed class EntityNotFoundException(string? message = null) : DomainException(message);
