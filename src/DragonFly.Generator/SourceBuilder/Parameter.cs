// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator.SourceBuilder;

/// <summary>
/// Parameter
/// </summary>
internal class Parameter
{
    public Parameter(string name, string type)
    {
        Name = name;
        Type = type;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    public string Type { get; set; }

    public override string ToString()
    {
        return $"{Type} {Name}";
    }
}
