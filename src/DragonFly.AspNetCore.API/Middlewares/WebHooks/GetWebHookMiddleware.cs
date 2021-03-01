using DragonFly.AspNetCore.API.Models;
using DragonFly.AspNetCore.API.Models.WebHooks;
using DragonFly.Content;
using DragonFly.Core.WebHooks;
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

namespace DragonFly.AspNetCore.API.Middlewares
{
    class GetWebHookMiddleware
    {
        private readonly RequestDelegate _next;

        public GetWebHookMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context, 
            IWebHookStorage contentStore, 
            JsonService jsonService)
        {
            Guid id = Guid.Parse((string)context.GetRouteValue("id"));

            WebHook result = await contentStore.GetAsync(id);

            RestWebHook restModel = result.ToRest();

            string json = jsonService.Serialize(restModel);

            await context.Response.WriteAsync(json);
        }
    }
}
