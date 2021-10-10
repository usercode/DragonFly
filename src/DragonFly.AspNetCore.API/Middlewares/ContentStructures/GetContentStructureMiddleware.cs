using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;
using DragonFly.Core.ContentStructures;
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

namespace DragonFly.AspNetCore.API.Middlewares.ContentStructures
{
    class GetContentStructureMiddleware
    {
        private readonly RequestDelegate _next;

        public GetContentStructureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IStructureStorage storage,
            JsonService jsonService)
        {
            ContentStructure entity;

            if (context.GetRouteValue("id") is string stringId)
            {
                Guid id = Guid.Parse(stringId);

                entity = await storage.GetStructureAsync(id);
            }
            else
            {
                string name = (string)context.GetRouteValue("name");

                entity = await storage.GetStructureAsync(name);
            }

            RestContentStructure restSchema = entity.ToRest();

            string json = jsonService.Serialize(restSchema);

            await context.Response.WriteAsync(json);
        }
    }
}
