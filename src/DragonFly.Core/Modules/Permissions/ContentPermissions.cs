// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class ContentPermissions
{
    public static readonly PermissionGroup ContentGroup = new PermissionGroup("Content");

    public static readonly Permission ManageContent = new Permission(ContentGroup, "ManageContent", "Manage content");

    public static readonly Permission QueryContent = new Permission(ContentGroup, "QueryContent", "Query content", ManageContent);
    public static readonly Permission ReadContent = new Permission(ContentGroup, "ReadContent", "Read content", ManageContent, QueryContent);
    public static readonly Permission CreateContent = new Permission(ContentGroup, "CreateContent", "Create content", ManageContent);
    public static readonly Permission UpdateContent = new Permission(ContentGroup, "UpdateContent", "Update content", ManageContent);
    public static readonly Permission DeleteContent = new Permission(ContentGroup, "DeleteContent", "Delete content", ManageContent);
    public static readonly Permission PublishContent = new Permission(ContentGroup, "PublishContent", "Publish content", ManageContent);
    public static readonly Permission UnpublishContent = new Permission(ContentGroup, "UnpublishContent", "Unpublish content", ManageContent);
}
