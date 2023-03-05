// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

public static class QueryResultExtensions
{
    public static QueryResult<TTarget> Convert<TSource, TTarget>(this QueryResult<TSource> sourceResult, Func<TSource, TTarget> converter)
    {
        QueryResult<TTarget> result = new QueryResult<TTarget>();
        result.Offset = sourceResult.Offset;
        result.Count = sourceResult.Count;
        result.TotalCount = sourceResult.TotalCount;
        result.Items = sourceResult.Items.Select(x => converter(x)).ToList();

        return result;
    }
}
