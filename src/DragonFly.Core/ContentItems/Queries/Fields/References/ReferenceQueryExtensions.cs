using DragonFly.Content.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// ReferenceQueryExtensions
    /// </summary>
    public static class ReferenceQueryExtensions
    {
        public static QueryParameters AddReferenceQuery(this QueryParameters queryParameters, string name, Guid id)
        {
            queryParameters.Fields.Add(new ReferenceQuery() { FieldName = name, ContentItemId = id });

            return queryParameters;
        }
    }
}
