// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// QueryResult
/// </summary>
/// <typeparam name="T"></typeparam>
public class QueryResult<T>
{
    public QueryResult()
    {
        Items = new List<T>();
    }
    
    /// <summary>
    /// Items
    /// </summary>
    public IList<T> Items { get; set; }

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
