using DragonFly.Content.Queries;
using DragonFly.Core.ContentItems.Queries.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public static class StringQueryExtensions
    {
        public static QueryParameters AddStringQuery(this QueryParameters queryParameters, string name, string pattern, StringQueryType type = StringQueryType.Equal)
        {
            queryParameters.Fields2.Add(new StringQuery() { FieldName = name, Pattern = pattern, Type = type });

            return queryParameters;
        }
    }
}
