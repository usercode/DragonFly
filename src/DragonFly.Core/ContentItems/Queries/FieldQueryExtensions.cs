using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DragonFly.Content.Queries
{
    public static class FieldQueryExtensions
    {
        public static ContentItemQuery AddFieldOrder(this ContentItemQuery queryParameters, string field, bool asc = true)
        {
            queryParameters.OrderFields.Add(new FieldOrder($"Fields.{field}", asc));

            return queryParameters;
        }


    }
}
