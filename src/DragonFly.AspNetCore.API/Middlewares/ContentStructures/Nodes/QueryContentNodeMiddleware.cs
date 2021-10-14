using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;
using DragonFly.Core.ContentStructures;
using DragonFly.Core.ContentStructures.Queries;
using DragonFly.Data;
using DragonFly.Data.Models;
using DragonFly.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares.ContentSchemas
{
    class QueryContentNodeMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryContentNodeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IStructureStorage storage,
            JsonService jsonService)
        {
            string structureName = (string)context.GetRouteValue("structure");

            var parentIdQuery = context.Request.Query["parentId"];
            Guid? parentId = null;

            if (parentIdQuery.Any() && string.IsNullOrWhiteSpace(parentIdQuery.First()) == false)
            {
                parentId = Guid.Parse(parentIdQuery.First());
            }

            QueryResult<ContentNode> items = await storage
                                                    .QueryAsync(new NodesQuery() { Structure = structureName, ParentId = parentId });

            QueryResult<RestContentNode> restQueryResult = new QueryResult<RestContentNode>();
            restQueryResult.Items = items.Items.Select(x => x.ToRest()).ToList();
            restQueryResult.Offset = items.Offset;
            restQueryResult.Count = items.Count;
            restQueryResult.TotalCount = items.TotalCount;

            string json = jsonService.Serialize(restQueryResult);

            await context.Response.WriteAsync(json);
        }
    }
}
