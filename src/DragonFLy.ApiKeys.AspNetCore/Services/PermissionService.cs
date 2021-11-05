using DragonFly.Permissions;
using DragonFly.Permissions.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DragonFLy.ApiKeys.AspNetCore.Services
{
    internal class PermissionService : IAuthorizePermissionService
    {
        public async Task<bool> AuthorizeAsync(ClaimsPrincipal principal, string permission)
        {


            return false;            
        }
    }
}
