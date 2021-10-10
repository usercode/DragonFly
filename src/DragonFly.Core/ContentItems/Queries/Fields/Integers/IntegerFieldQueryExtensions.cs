using DragonFly.Content.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// IntegerFieldQueryExtensions
    /// </summary>
    public static class IntegerFieldQueryExtensions
    {
        public static ContentItemQuery AddIntegerQuery(this ContentItemQuery queryParameters, string name, int value)
        {
            //queryParameters.Fields2.Add(new IntegerQuery() { FieldName = name, Value = value, Operator = @operator });

            return queryParameters;
        }

    }
}
