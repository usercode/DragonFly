using DragonFly.AspNetCore.API.Models;
using DragonFly.AspNetCore.API.Models.WebHooks;
using DragonFly.AspNetCore.Exports;
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
    class CreateWebHookMiddleware
    {
        private readonly RequestDelegate _next;

        public CreateWebHookMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context, 
            IWebHookStorage contentStore,
            JsonService jsonService)
        {
            RestWebHook input = await jsonService.Deserialize<RestWebHook>(context.Request.Body);

            WebHook model = input.ToModel();

            await contentStore.CreateAsync(model);

            ResourceCreated result = new ResourceCreated() { Id = model.Id };

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }
    }
}
