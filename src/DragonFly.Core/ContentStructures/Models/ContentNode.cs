// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentNode
/// </summary>
public class ContentNode : ContentBase
{
    /// <summary>
    /// StructureName
    /// </summary>
    public Guid? Structure { get; set; }

    /// <summary>
    /// Parent
    /// </summary>
    public ContentNode? Parent { get; set; }

    /// <summary>
    /// Target
    /// </summary>
    public IContentNodeTarget? Target { get; set; }
}
