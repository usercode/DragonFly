// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class WebHookPermissions
{
    public static readonly PermissionGroup WebHookGroup = new PermissionGroup("WebHooks");

    public static readonly Permission ManageWebHook = new Permission(WebHookGroup, "WebHook:*", "Manage webhook");

    public static readonly Permission QueryWebHook = new Permission(WebHookGroup, "WebHook:Query", "Query webhook", ManageWebHook);
    public static readonly Permission ReadWebHook = new Permission(WebHookGroup, "WebHook:Read", "Read webhook", ManageWebHook, QueryWebHook);
    public static readonly Permission CreateWebHook = new Permission(WebHookGroup, "WebHook:Create", "Create webhook", ManageWebHook);
    public static readonly Permission UpdateWebHook = new Permission(WebHookGroup, "WebHook:Update", "Update webhook", ManageWebHook);
    public static readonly Permission DeleteWebHook = new Permission(WebHookGroup, "WebHook:Delete", "Delete webhook", ManageWebHook);
}
