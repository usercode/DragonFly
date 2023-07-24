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
        string name = $"Content.{schema}:{action}";
        string displayName = $"{action} {schema} content";
        PermissionGroup group = new PermissionGroup($"Content ({schema})");

        Permission permission = action switch
        {
            ContentAction.Query => new Permission(group, name, displayName, ManageContent, QueryContent),
            ContentAction.Read => new Permission(group, name, displayName, ManageContent, ReadContent),            
            ContentAction.Create => new Permission(group, name, displayName, ManageContent, CreateContent),
            ContentAction.Update => new Permission(group, name, displayName, ManageContent, UpdateContent),
            ContentAction.Delete => new Permission(group, name, displayName, ManageContent, DeleteContent),
            ContentAction.Publish => new Permission(group, name, displayName, ManageContent, PublishContent),
            ContentAction.Unpublish => new Permission(group, name, displayName, ManageContent, UnpublishContent),
            _ => throw new Exception()
        };

        return permission;
    }
}

/// <summary>
/// ContentAction
/// </summary>
public enum ContentAction
{
    Query,
    Read,
    Create,
    Update,
    Delete,
    Publish,
    Unpublish
}
