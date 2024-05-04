// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using SmartResults;

namespace DragonFly.AspNetCore;

/// <summary>
/// PermissionError
/// </summary>
public class PermissionError : Error
{
    public PermissionError(Permission permission)
        : base($"Access denied for \"{permission.Name}\"")
    {
        Permission = permission;
    }

    /// <summary>
    /// Permission
    /// </summary>
    public Permission Permission { get; }


}
