using DragonFly.Content.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// StringQueryExtensions
    /// </summary>
    public static class StringQueryExtensions
    {
        public static QueryParameters AddStringQuery(this QueryParameters queryParameters, string name, string pattern, StringQueryType type = StringQueryType.Equals)
        {
            queryParameters.Fields.Add(new StringQuery() { FieldName = name, Pattern = pattern, Type = type });

            return queryParameters;
        }
    }
}
