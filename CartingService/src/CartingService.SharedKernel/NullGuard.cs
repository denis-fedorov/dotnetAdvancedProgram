using System.Runtime.CompilerServices;

namespace CartingService.SharedKernel;

public static class NullGuard
{
    public static T ThrowIfNull<T>(T obj, [CallerArgumentExpression("obj")] string? paramName = null)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(paramName);
        }

        return obj;
    }
}