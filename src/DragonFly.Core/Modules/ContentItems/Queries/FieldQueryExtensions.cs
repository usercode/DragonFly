// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class FieldQueryExtensions
{
    public static TContentQuery OrderBy<TContentQuery>(this TContentQuery query, string field, bool asc = true, bool isCustomField = true)
        where TContentQuery : ContentQuery
    {
        if (isCustomField)
        {
            query.OrderFields.Add(new FieldOrder($"Fields.{field}", asc));
        }
        else
        {
            query.OrderFields.Add(new FieldOrder(field, asc));
        }

        return query;
    }

    public static TContentQuery Top<TContentQuery>(this TContentQuery query, int value)
        where TContentQuery : ContentQuery
    {
        query.Top = value;

        return query;
    }

    public static TContentQuery Skip<TContentQuery>(this TContentQuery query, int value)
        where TContentQuery : ContentQuery
    {
        query.Skip = value;

        return query;
    }

    public static TContentQuery Published<TContentQuery>(this TContentQuery query, bool value)
        where TContentQuery : ContentQuery
    {
        query.Published = value;

        return query;
    }
}
