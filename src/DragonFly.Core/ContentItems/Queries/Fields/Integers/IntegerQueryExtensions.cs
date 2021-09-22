using DragonFly.Content.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public static class IntegerQueryExtensions
    {
        public static QueryParameters AddIntegerQuery(this QueryParameters queryParameters, string name, int value)
        {
            //queryParameters.Fields2.Add(new IntegerQuery() { FieldName = name, Value = value, Operator = @operator });

            return queryParameters;
        }

    }
}
