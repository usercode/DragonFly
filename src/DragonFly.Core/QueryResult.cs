// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// QueryResult
/// </summary>
/// <typeparam name="TModel"></typeparam>
public class QueryResult<TModel>
{
    public QueryResult()
    {
        Items = new List<TModel>();
    }
    
    /// <summary>
    /// Items
    /// </summary>
    public IList<TModel> Items { get; set; }

    /// <summary>
    /// Offset
    /// </summary>
    public long Offset { get; set; }

    /// <summary>
    /// Count
    /// </summary>
    public long Count { get; set; }

    /// <summary>
    /// TotalCount
    /// </summary>
    public long TotalCount { get; set; }
}
