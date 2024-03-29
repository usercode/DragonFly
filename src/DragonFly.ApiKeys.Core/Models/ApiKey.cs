﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using System.Collections.Generic;

namespace DragonFly.ApiKeys;

/// <summary>
/// ApiKey
/// </summary>
public class ApiKey : Entity<ApiKey>
{
    public ApiKey()
    {
        Name = string.Empty;
        Value = string.Empty;
        Permissions = new List<string>();
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Permissions
    /// </summary>
    public IList<string> Permissions { get; set; }
}
