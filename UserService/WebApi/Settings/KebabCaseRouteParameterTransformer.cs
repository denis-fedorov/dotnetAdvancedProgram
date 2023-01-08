using System.Text.RegularExpressions;

namespace WebApi.Settings;

public class KebabCaseRouteParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        var route = value?.ToString();

        return route == null
            ? null
            : Regex.Replace(route, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}