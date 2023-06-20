// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

/// <summary>
/// Modifier
/// </summary>
public class Modifier
{
    public static readonly Modifier Public = Get("public");

    public static readonly Modifier Private = Get("private");

    public static readonly Modifier Internal = Get("internal");

    public static Modifier Get(string name) => new Modifier(name);

    private Modifier(string name)
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
