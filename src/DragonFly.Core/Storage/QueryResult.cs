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
    /// <summary>
    /// Items
    /// </summary>
    public IList<TModel> Items { get; set; } = new List<TModel>();

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
