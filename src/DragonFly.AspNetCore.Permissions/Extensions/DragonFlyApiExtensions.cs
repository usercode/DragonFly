using DragonFly.Content;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public static class DragonFlyApiExtensions
    {
        public static async Task AuthorizeAsync(this IDragonFlyApi api, string policyname)
        {
            IAuthorizationService authorizationService = api.ServiceProvider.GetRequiredService<IAuthorizationService>();
            IHttpContextAccessor httpContextAccessor = api.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

            var result = await authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext!.User, policyname);

            if (result.Succeeded == false)
            {
                throw new Exception($"Access denied: {policyname}");
            }
        }
    }
}
