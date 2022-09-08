using Microsoft.AspNetCore.Http;

namespace DragonFly.AspNet.Middleware;

class RequireAuthentificationMiddleware
{
    private readonly RequestDelegate _next;


    public RequireAuthentificationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == false)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
        else
        {
            PermissionState.Enable();

            await _next(context);
        }
    }
}
