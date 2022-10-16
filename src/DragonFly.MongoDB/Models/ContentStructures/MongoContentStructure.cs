// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

/// <summary>
/// MongoContentStructure
/// </summary>
public class MongoContentStructure : MongoContentBase
{
    public MongoContentStructure()
    {
        Name = string.Empty;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
}
