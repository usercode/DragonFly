// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class BackgroundTaskPermissions
{
    public static readonly PermissionGroup BackgroundTaskGroup = new PermissionGroup("Background tasks");

    public static readonly Permission ManageBackgroundTask = new Permission(BackgroundTaskGroup, "ManageBackgroundTask", "Manage background task");

    public static readonly Permission QueryBackgroundTask = new Permission(BackgroundTaskGroup, "QueryBackgroundTask", "Query background task", ManageBackgroundTask);
    public static readonly Permission CancelBackgroundTask = new Permission(BackgroundTaskGroup, "CancelBackgroundTask", "Cancel background task", ManageBackgroundTask);
}
