using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DragonFly.Content.Queries
{
    public static class QueryExtensions
    {
        public static QueryParameters AddSimpleFieldEquals(this QueryParameters queryParameters, string name, string value)
        {
            queryParameters.Fields.Add(new FieldQuery($"Fields.{name}", value, QueryFieldType.String));

            return queryParameters;
        }

        public static QueryParameters AddSimpleFieldEquals(this QueryParameters queryParameters, string name, double value)
        {
            queryParameters.Fields.Add(new FieldQuery($"Fields.{name}", value.ToString(), QueryFieldType.Double));

            return queryParameters;
        }

        public static QueryParameters AddReferenceEquals(this QueryParameters queryParameters, string name, Guid id)
        {
            queryParameters.Fields.Add(new FieldQuery($"Fields.{name}.Id", id.ToString(), QueryFieldType.Guid));

            return queryParameters;
        }

        public static QueryParameters AddFieldOrder(this QueryParameters queryParameters, string field, bool asc = true)
        {
            queryParameters.OrderFields.Add(new FieldOrder($"Fields.{field}", asc));

            return queryParameters;
        }


    }
}
