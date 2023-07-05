// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

/// <summary>
/// ClassItem
/// </summary>
internal class ClassItem
{
    public ClassItem(string @namespace, string name)
    {
        Name = name;
        Namespace = @namespace;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Namespace
    /// </summary>
    public string Namespace { get; set; }
}
