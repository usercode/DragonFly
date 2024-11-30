// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// Query result for <typeparamref name="T"/>.
/// </summary>
public class QueryResult<T>
{    
    /// <summary>
    /// Items
    /// </summary>
    public IList<T> Items { get; set; } = new List<T>();

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

    public override string ToString()
    {
        return $"{typeof(T).Name} / Count: {Items.Count}";
    }
}
