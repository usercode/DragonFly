// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Permissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SmartResults;

namespace DragonFly.API;

static class BackgroundTaskApiExtensions
{
    public static void MapBackgroundTaskApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("task");

        groupRoute.MapPost("query", MapQuery).RequirePermission(BackgroundTaskPermissions.QueryBackgroundTask);
        groupRoute.MapPost("{id}/cancel", MapCancel).RequirePermission(BackgroundTaskPermissions.CancelBackgroundTask);
    }

    private static async Task<IResult> MapQuery(IBackgroundTaskManager service)
    {
        return (await service.GetTasksAsync()).ToHttpResult();
    }

    private static async Task<IResult> MapCancel(int id, IBackgroundTaskManager service)
    {
        return (await service.CancelAsync(id)).ToHttpResult();
    }
}
