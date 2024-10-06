namespace ScaleUp.Core.SharedKernel.Extensions;

public static class ExceptionExtensions
{
    public static Exception GetInnermostException(this Exception e)
    {
        while (e.InnerException != null)
        {
            e = e.InnerException;
        }
        return e;
    }
}