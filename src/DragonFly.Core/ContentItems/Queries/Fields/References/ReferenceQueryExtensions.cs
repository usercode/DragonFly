using DragonFly.Core.ContentItems.Queries.Fields.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content.Queries
{
    public static class ReferenceQueryExtensions
    {
        public static QueryParameters AddReferenceQuery(this QueryParameters queryParameters, string name, Guid id)
        {
            queryParameters.Fields2.Add(new ReferenceQuery() { FieldName = name, Id = id });

            return queryParameters;
        }
    }
}
