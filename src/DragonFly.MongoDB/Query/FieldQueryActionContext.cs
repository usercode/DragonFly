// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Driver;

namespace DragonFly.MongoDB.Query;

/// <summary>
/// FieldQueryActionContext
/// </summary>
public class FieldQueryActionContext
{
    public FieldQueryActionContext()
    {
        Filters = new List<FilterDefinition<MongoContentItem>>();
    }

    /// <summary>
    /// Filter
    /// </summary>
    public IList<FilterDefinition<MongoContentItem>> Filters { get; }
}
