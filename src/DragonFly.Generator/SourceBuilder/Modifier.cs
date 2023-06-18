// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

/// <summary>
/// Modifier
/// </summary>
public class Modifier
{
    public static readonly Modifier Public = new Modifier("public");

    public static readonly Modifier Private = new Modifier("private");

    public static readonly Modifier Internal = new Modifier("internal");

    public Modifier(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; }

    public override string ToString()
    {
        return Name;
    }
}
