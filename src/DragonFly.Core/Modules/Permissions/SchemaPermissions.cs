// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class SchemaPermissions
{
    public static readonly PermissionGroup SchemaGroup = new PermissionGroup("Schema");

    public static readonly Permission ManageSchema = new Permission(SchemaGroup, "ManageSchema", "Manage schema");

    public static readonly Permission QuerySchema = new Permission(SchemaGroup, "QuerySchema", "Query schema", ManageSchema);
    public static readonly Permission ReadSchema = new Permission(SchemaGroup, "ReadSchema", "Read schema", ManageSchema, QuerySchema);
    public static readonly Permission CreateSchema = new Permission(SchemaGroup, "CreateSchema", "Create schema", ManageSchema);
    public static readonly Permission UpdateSchema = new Permission(SchemaGroup, "UpdateSchema", "Update schema", ManageSchema);
    public static readonly Permission DeleteSchema = new Permission(SchemaGroup, "DeleteSchema", "Delete schema", ManageSchema);
}
