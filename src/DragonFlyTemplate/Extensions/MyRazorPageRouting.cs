using Microsoft.AspNetCore.Mvc.Routing;

namespace DragonFlyTemplate.Extensions;

public class MyRazorPageRouting : DynamicRouteValueTransformer
{
    public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
    {
        return await Task.Run(() =>
        {
            values["page"] = "/Start";

            return values;
        });
    }
}
