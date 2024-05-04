// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Permissions;
using Microsoft.AspNetCore.Builder;
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

    private static async Task<BackgroundTaskInfo[]> MapQuery(IBackgroundTaskManager service)
    {
        return await service.GetTasksAsync();
    }

    private static async Task MapCancel(int id, IBackgroundTaskManager service)
    {
        await service.CancelAsync(id);
    }
}
