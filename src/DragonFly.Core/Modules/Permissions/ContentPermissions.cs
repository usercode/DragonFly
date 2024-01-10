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

    /// <summary>
    /// Creates permission for a specified schema and action.
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Permission Create(string schema, ContentAction action)
    {
        ArgumentException.ThrowIfNullOrEmpty(schema);

        static string createName(string schema, ContentAction action) => $"Content.{schema}:{action}";
        static string createDisplayName(string schema, ContentAction action) => $"{action} {schema} content";

        string name = createName(schema, action);
        string displayName = createDisplayName(schema, action);
        PermissionGroup group = new PermissionGroup($"Content ({schema})");

        Permission manage = new Permission(group, createName(schema, ContentAction.Manage), createDisplayName(schema, ContentAction.Manage), ManageContent);
        Permission query = new Permission(group, createName(schema, ContentAction.Query), createDisplayName(schema, ContentAction.Query), manage, QueryContent);

        Permission permission = action switch
        {
            ContentAction.Manage => manage,
            ContentAction.Query => query,
            ContentAction.Read => new Permission(group, name, displayName, manage, query, ReadContent),
            ContentAction.Create => new Permission(group, name, displayName, manage, CreateContent),
            ContentAction.Update => new Permission(group, name, displayName, manage, UpdateContent),
            ContentAction.Delete => new Permission(group, name, displayName, manage, DeleteContent),
            ContentAction.Publish => new Permission(group, name, displayName, manage, PublishContent),
            ContentAction.Unpublish => new Permission(group, name, displayName, manage, UnpublishContent),
            _ => throw new Exception($"Unknown permission action: {action}")
        };

        return permission;
    }
}

/// <summary>
/// ContentAction
/// </summary>
public enum ContentAction
{
    Manage,
    Query,
    Read,
    Create,
    Update,
    Delete,
    Publish,
    Unpublish
}
