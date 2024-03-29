﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// PermissionGroup
/// </summary>
public class PermissionGroup
{
    public PermissionGroup()
    {        
    }

    public PermissionGroup(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
