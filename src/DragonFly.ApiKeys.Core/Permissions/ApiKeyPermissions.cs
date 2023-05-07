// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;

namespace DragonFLy.ApiKeys.Permissions;

public static class ApiKeyPermissions
{
    public static readonly PermissionGroup ApiKeyGroup = new PermissionGroup("API keys");

    public static readonly Permission ManageApiKey = new Permission(ApiKeyGroup, "ManageApiKey", "Manage api key");

    public static readonly Permission QueryApiKey = new Permission(ApiKeyGroup, "QueryApiKey", "Query api key", ManageApiKey);
    public static readonly Permission ReadApiKey = new Permission(ApiKeyGroup, "ReadApiKey", "Read api key", ManageApiKey, QueryApiKey);    
    public static readonly Permission CreateApiKey = new Permission(ApiKeyGroup, "CreateApiKey", "Create api key", ManageApiKey);
    public static readonly Permission UpdateApiKey = new Permission(ApiKeyGroup, "UpdateApiKey", "Update api key", ManageApiKey);
    public static readonly Permission DeleteApiKey = new Permission(ApiKeyGroup, "DeleteApiKey", "Delete api key", ManageApiKey);
}
