// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;

namespace DragonFly.ApiKeys.Permissions;

public static class ApiKeyPermissions
{
    public static readonly PermissionGroup ApiKeyGroup = new PermissionGroup("API keys");

    public static readonly Permission ManageApiKey = new Permission(ApiKeyGroup, "ApiKey:*", "Manage api key");

    public static readonly Permission QueryApiKey = new Permission(ApiKeyGroup, "ApiKey:Query", "Query api key", ManageApiKey);
    public static readonly Permission ReadApiKey = new Permission(ApiKeyGroup, "ApiKey:Read", "Read api key", ManageApiKey, QueryApiKey);    
    public static readonly Permission CreateApiKey = new Permission(ApiKeyGroup, "ApiKey:Create", "Create api key", ManageApiKey);
    public static readonly Permission UpdateApiKey = new Permission(ApiKeyGroup, "ApiKey:Update", "Update api key", ManageApiKey);
    public static readonly Permission DeleteApiKey = new Permission(ApiKeyGroup, "ApiKey:Delete", "Delete api key", ManageApiKey);
}
