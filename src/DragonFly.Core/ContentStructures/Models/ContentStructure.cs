// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentStructure
/// </summary>
public class ContentStructure : ContentBase<ContentStructure>
{
    public ContentStructure()
        : this(string.Empty)
    {

    }

    public ContentStructure(string name)
    {
        Name = name;
    }


    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }
}
