using DragonFly.Core.ContentItems.Queries.Fields.Integers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content.Queries
{
    public static class IntegerQueryExtensions
    {
        public static QueryParameters AddIntegerQuery(this QueryParameters queryParameters, string name, int value, IntegerQueryOperator @operator)
        {
            queryParameters.Fields2.Add(new IntegerQuery() { FieldName = name, Value = value, Operator = @operator });

            return queryParameters;
        }

    }
}
