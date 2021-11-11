using DragonFly.Content.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// AssetFieldQueryExtensions
    /// </summary>
    public static class AssetFieldQueryExtensions
    {
        public static ContentItemQuery AddAssetQuery(this ContentItemQuery queryParameters, string name, Guid? id)
        {
            queryParameters.Fields.Add(new AssetFieldQuery() { FieldName = name, AssetId = id });

            return queryParameters;
        }
    }
}
