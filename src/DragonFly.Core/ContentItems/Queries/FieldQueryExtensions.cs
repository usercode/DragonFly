// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

public static class FieldQueryExtensions
{
    public static TContentQuery AddFieldOrder<TContentQuery>(this TContentQuery queryParameters, string field, bool asc = true)
        where TContentQuery : IContentQuery
    {
        queryParameters.OrderFields.Add(new FieldOrder($"Fields.{field}", asc));

        return queryParameters;
    }

    public static TContentQuery Top<TContentQuery>(this TContentQuery query, int value)
        where TContentQuery : IContentQuery
    {
        query.Top = value;

        return query;
    }

    public static TContentQuery Skip<TContentQuery>(this TContentQuery query, int value)
        where TContentQuery : IContentQuery
    {
        query.Skip = value;

        return query;
    }

    public static TContentQuery Published<TContentQuery>(this TContentQuery query, bool value)
        where TContentQuery : IContentQuery
    {
        query.Published = value;

        return query;
    }
}
