using DragonFly.Content.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// StringFieldQueryExtensions
    /// </summary>
    public static class StringFieldQueryExtensions
    {
        public static ContentItemQuery AddStringQuery(this ContentItemQuery queryParameters, string name, string pattern, StringFieldQueryType type = StringFieldQueryType.Equals)
        {
            queryParameters.Fields.Add(new StringFieldQuery() { FieldName = name, Pattern = pattern, PatternType = type });

            return queryParameters;
        }
    }
}
