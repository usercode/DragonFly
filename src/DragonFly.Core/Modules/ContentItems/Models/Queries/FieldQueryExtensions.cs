// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class FieldQueryExtensions
{
    /// <summary>
    /// Orders models by a specified field.
    /// </summary>
    /// <typeparam name="TContentQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="field"></param>
    /// <param name="asc"></param>
    /// <param name="isCustomField"></param>
    /// <returns></returns>
    public static TContentQuery OrderBy<TContentQuery>(this TContentQuery query, string field, bool asc = true, bool isCustomField = true)
        where TContentQuery : ContentQuery
    {
        if (isCustomField)
        {
            query.OrderFields.Add(new FieldOrder($"{nameof(ContentItem.Fields)}.{field}", asc));
        }
        else
        {
            query.OrderFields.Add(new FieldOrder(field, asc));
        }

        return query;
    }

    /// <summary>
    /// Selects the schema.
    /// </summary>
    /// <typeparam name="TContentQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static TContentQuery Schema<TContentQuery>(this TContentQuery query, string name)
        where TContentQuery : ContentQuery
    {
        query.Schema = name;

        return query;
    }

    /// <summary>
    /// Selects a specified number of models.
    /// </summary>
    /// <typeparam name="TContentQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TContentQuery Top<TContentQuery>(this TContentQuery query, int value)
        where TContentQuery : ContentQuery
    {
        query.Top = value;

        return query;
    }

    /// <summary>
    /// Skips a specified number of models.
    /// </summary>
    /// <typeparam name="TContentQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TContentQuery Skip<TContentQuery>(this TContentQuery query, int value)
        where TContentQuery : ContentQuery
    {
        query.Skip = value;

        return query;
    }

    /// <summary>
    /// Filters all published or unpublished models.
    /// </summary>
    /// <typeparam name="TContentQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TContentQuery Published<TContentQuery>(this TContentQuery query, bool value)
        where TContentQuery : ContentQuery
    {
        query.Published = value;

        return query;
    }
}
