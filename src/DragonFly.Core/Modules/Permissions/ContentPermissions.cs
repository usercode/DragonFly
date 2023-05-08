// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class ContentPermissions
{
    public static readonly PermissionGroup ContentGroup = new PermissionGroup("Content");

    public static readonly Permission ManageContent = new Permission(ContentGroup, "Content:*", "Manage content");

    public static readonly Permission QueryContent = new Permission(ContentGroup, "Content:Query", "Query content", ManageContent);
    public static readonly Permission ReadContent = new Permission(ContentGroup, "Content:Read", "Read content", ManageContent, QueryContent);
    public static readonly Permission CreateContent = new Permission(ContentGroup, "Content:Create", "Create content", ManageContent);
    public static readonly Permission UpdateContent = new Permission(ContentGroup, "Content:Update", "Update content", ManageContent);
    public static readonly Permission DeleteContent = new Permission(ContentGroup, "Content:Delete", "Delete content", ManageContent);
    public static readonly Permission PublishContent = new Permission(ContentGroup, "Content:Publish", "Publish content", ManageContent);
    public static readonly Permission UnpublishContent = new Permission(ContentGroup, "Content:Unpublish", "Unpublish content", ManageContent);
}
