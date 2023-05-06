// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public interface IPermissionService
{
    Task<IEnumerable<Permission>> GetPermissionsAsync();
}
