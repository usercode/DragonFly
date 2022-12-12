// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

public static class FieldQueryExtensions
{
    public static ContentItemQuery AddFieldOrder(this ContentItemQuery queryParameters, string field, bool asc = true)
    {
        queryParameters.OrderFields.Add(new FieldOrder($"Fields.{field}", asc));

        return queryParameters;
    }

    public static ContentItemQuery Top(this ContentItemQuery query, int value)
    {
        query.Top = value;

        return query;
    }

    public static ContentItemQuery Skip(this ContentItemQuery query, int value)
    {
        query.Skip = value;

        return query;
    }

    public static ContentItemQuery Published(this ContentItemQuery query, bool value)
    {
        query.Published = value;

        return query;
    }
}
