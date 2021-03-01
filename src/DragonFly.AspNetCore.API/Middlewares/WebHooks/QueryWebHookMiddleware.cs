using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Models;
using DragonFly.AspNetCore.API.Models.WebHooks;
using DragonFly.Content;
using DragonFly.Core.WebHooks;
using DragonFly.Data;
using DragonFly.Data.Models;
using DragonFly.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares.ContentSchemas
{
    class QueryWebHookMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryWebHookMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IWebHookStorage schemaStorage,
            JsonService jsonService)
        {
            QueryResult<WebHook> items = await schemaStorage
                                                    .QueryAsync(new WebHookQuery());

            QueryResult<RestWebHook> restQueryResult = new QueryResult<RestWebHook>();
            restQueryResult.Items = items.Items.Select(x => x.ToRest()).ToList();
            restQueryResult.Offset = items.Offset;
            restQueryResult.Count = items.Count;
            restQueryResult.TotalCount = items.TotalCount;

            string json = jsonService.Serialize(restQueryResult);

            await context.Response.WriteAsync(json);
        }
    }
}
