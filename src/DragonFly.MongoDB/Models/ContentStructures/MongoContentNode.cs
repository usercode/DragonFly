// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

/// <summary>
/// MongoContentNode
/// </summary>
public class MongoContentNode : MongoContentBase
{
    public MongoContentNode()
    {
    }

    /// <summary>
    /// Structure
    /// </summary>
    public Guid? Structure { get; set; }

    /// <summary>
    /// Parent
    /// </summary>
    public Guid? Parent { get; set; }

    /// <summary>
    /// Target
    /// </summary>
    public IContentNodeTarget? Target { get; set; }
}
