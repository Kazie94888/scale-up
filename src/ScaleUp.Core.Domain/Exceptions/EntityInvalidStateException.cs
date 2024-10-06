namespace ScaleUp.Core.Domain.Exceptions;

public sealed class EntityInvalidStateException(string? message = null) : DomainException(message);