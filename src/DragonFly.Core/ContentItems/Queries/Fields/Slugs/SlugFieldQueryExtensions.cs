// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// SlugFieldQueryExtensions
/// </summary>
public static class SlugFieldQueryExtensions
{
    public static ContentItemQuery AddSlugQuery(this ContentItemQuery queryParameters, string name, string value)
    {
        queryParameters.Fields.Add(new SlugFieldQuery() { FieldName = name, Value = value });

        return queryParameters;
    }
}
