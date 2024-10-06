namespace ScaleUp.Core.Domain.Exceptions;

public class NullEntityException<T> : Exception
{
    public NullEntityException()
        : base($"{typeof(T).Name} is null.")
    {
    }

    public static T Throw()
    {
        throw new NullEntityException<T>();
    }
}
