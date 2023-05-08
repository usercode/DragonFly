// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class SchemaPermissions
{
    public static readonly PermissionGroup SchemaGroup = new PermissionGroup("Schema");

    public static readonly Permission ManageSchema = new Permission(SchemaGroup, "Schema:*", "Manage schema");

    public static readonly Permission QuerySchema = new Permission(SchemaGroup, "Schema:Query", "Query schema", ManageSchema);
    public static readonly Permission ReadSchema = new Permission(SchemaGroup, "Schema:Read", "Read schema", ManageSchema, QuerySchema);
    public static readonly Permission CreateSchema = new Permission(SchemaGroup, "Schema:Create", "Create schema", ManageSchema);
    public static readonly Permission UpdateSchema = new Permission(SchemaGroup, "Schema:Update", "Update schema", ManageSchema);
    public static readonly Permission DeleteSchema = new Permission(SchemaGroup, "Schema:Delete", "Delete schema", ManageSchema);
}
