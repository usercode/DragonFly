// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.API;

static class BackgroundTaskApiExtensions
{
    public static void MapBackgroundTaskApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("task");

        groupRoute.MapPost("query", MapQuery).RequireAuthorization(BackgroundTaskPermissions.BackgroundTaskQuery);
        groupRoute.MapPost("{id}/cancel", MapCancel).RequireAuthorization(BackgroundTaskPermissions.BackgroundTaskCancel);
    }

    private static async Task<IEnumerable<BackgroundTaskInfo>> MapQuery(HttpContext context, IBackgroundTaskManager service)
    {
        return await service.GetTasksAsync();
    }

    private static async Task MapCancel(int id, IBackgroundTaskManager service)
    {
        await service.CancelAsync(id);
    }
}
