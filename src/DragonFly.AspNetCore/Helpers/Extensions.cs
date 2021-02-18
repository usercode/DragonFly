using DragonFly.Content.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragonFly.AspNetCore
{
    public static class Extensions
    {
        public static QueryParameters GetQuery(this HttpRequest request)
        {
            QueryParameters result = new QueryParameters();

            StringValues skip = request.Query["$skip"];
            StringValues top = request.Query["$top"];

            if (skip.Count > 0)
            {
                result.Skip = int.Parse(skip.First());
            }

            if (top.Count > 0)
            {
                result.Top = int.Parse(top.First());
            }

            return result;
        }
    }
}
