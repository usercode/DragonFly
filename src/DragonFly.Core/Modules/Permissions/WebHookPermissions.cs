// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class WebHookPermissions
{
    public static readonly PermissionGroup WebHookGroup = new PermissionGroup("WebHooks");

    public static readonly Permission ManageWebHook = new Permission(WebHookGroup, "ManageWebHook", "Manage webhook");

    public static readonly Permission QueryWebHook = new Permission(WebHookGroup, "QueryWebHook", "Query webhook", ManageWebHook);
    public static readonly Permission ReadWebHook = new Permission(WebHookGroup, "ReadWebHook", "Read webhook", ManageWebHook, QueryWebHook);
    public static readonly Permission CreateWebHook = new Permission(WebHookGroup, "CreateWebHook", "Create webhook", ManageWebHook);
    public static readonly Permission UpdateWebHook = new Permission(WebHookGroup, "UpdateWebHook", "Update webhook", ManageWebHook);
    public static readonly Permission DeleteWebHook = new Permission(WebHookGroup, "DeleteWebHook", "Delete webhook", ManageWebHook);
}
