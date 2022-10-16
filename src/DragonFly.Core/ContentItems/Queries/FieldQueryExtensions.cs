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


}
