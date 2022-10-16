// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB.Index;

/// <summary>
/// FieldIndex
/// </summary>
public class FieldIndex
{
    public FieldIndex(string? name, bool unique)
    {
        Name = name;
        Unique = unique;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }    

    /// <summary>
    /// Unique
    /// </summary>
    public bool Unique { get; set; }

    public string CreateIndexPath(string fieldName)
    {
        if (Name == null)
        {
            return $"{nameof(MongoContentItem.Fields)}.{fieldName}";
        }
        else
        {
            return $"{nameof(MongoContentItem.Fields)}.{fieldName}.{Name}";
        }
    }

    public string CreateIndexName(string fieldName)
    {
        if (Name == null)
        {
            return $"{fieldName}";
        }
        else
        {
            return $"{fieldName}_{Name}";
        }
    }
}
