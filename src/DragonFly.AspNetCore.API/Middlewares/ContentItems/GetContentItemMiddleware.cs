﻿using DragonFly.ContentTypes;
using DragonFly.Data;
using DragonFly.Data.Content;
using DragonFly.Data.Models;
using DragonFly.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Rest.Middlewares
{
    class GetContentItemMiddleware
    {
        private readonly RequestDelegate _next;

        public GetContentItemMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context, 
            IContentStorage contentStore, 
            ISchemaStorage schemaStorage,
            JsonService jsonService)
        {
            Guid contentId = Guid.Parse((string)context.GetRouteValue("id"));
            string schema = (string)context.GetRouteValue("schema");

            ContentItem result = await contentStore.GetContentItemAsync(schema, contentId);
            ContentSchema schemaModel = await schemaStorage.GetContentSchemaByNameAsync(schema);

            result.ApplySchema(schemaModel);

            RestContentItem restModel = result.ToRest();

            string json = jsonService.Serialize(restModel);

            await context.Response.WriteAsync(json);
        }
    }
}
