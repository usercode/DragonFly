﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization;

namespace DragonFly.BlockField;

/// <summary>
/// Block
/// </summary>
public abstract class Block
{
    [JsonPropertyOrder(-1)]
    public virtual string Type => GetType().Name;

    private string? _name;

    public string Name
    {
        get
        {
            if (_name == null)
            {
                _name = CreateName();
            }

            return _name;
        }
    }

    protected virtual string CreateName()
    {
        string name = GetType().Name;

        string blockSuffix = "Block";

        if (name.EndsWith(blockSuffix))
        {
            name = name[0..^blockSuffix.Length];
        }

        return name;
    }

    public virtual string CssIcon => "bi bi-app";

    public override string ToString()
    {
        return Name;
    }
}
