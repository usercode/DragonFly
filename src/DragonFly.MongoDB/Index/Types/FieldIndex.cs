// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Driver;

namespace DragonFly.MongoDB.Index;

/// <summary>
/// FieldIndex
/// </summary>
public abstract class FieldIndex
{
    /// <summary>
    /// Unique
    /// </summary>
    public bool Unique { get; set; }

    /// <summary>
    /// FieldType
    /// </summary>
    public abstract Type FieldType { get; }

    public abstract Task CreateIndexAsync(IMongoIndexManager<MongoContentItem> indexManager, string fieldName);

    protected string CreateIndexPath(string fieldName)
    {
        return $"{nameof(MongoContentItem.Fields)}.{fieldName}";
    }
}
