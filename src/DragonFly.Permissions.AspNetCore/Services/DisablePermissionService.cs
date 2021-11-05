using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Permissions.AspNetCore.Services
{
    class DisablePermissionService : IAuthorizePermissionService
    {
        public DisablePermissionService(IAuthorizePermissionService permissionService)
        {
            Service = permissionService;
        }

        private IAuthorizePermissionService Service { get; }

        public async Task<bool> AuthorizeAsync(ClaimsPrincipal principal, string permission)
        {
            if (DisablePermissionState.Disabled.Value)
            {
                return true;
            }

            return await Service.AuthorizeAsync(principal, permission);
        }
    }
}
