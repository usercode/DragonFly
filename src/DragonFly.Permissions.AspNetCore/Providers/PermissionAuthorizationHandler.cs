using DragonFly.Permissions.AspNetCore.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Permissions.AspNetCore.Providers
{
    class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler(IEnumerable<IPermissionAuthorizationService> permissionServices)
        {
            PermissionServices = permissionServices;
        }

        private IEnumerable<IPermissionAuthorizationService> PermissionServices { get; }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (DisablePermissionState.Disabled.Value)
            {
                context.Succeed(requirement);
                return;
            }

            bool result = false;

            foreach (IPermissionAuthorizationService permission in PermissionServices)
            {
                result = await permission.AuthorizeAsync(context.User, requirement.Permission);

                if (result == true)
                {
                    break;
                }
            }

            if (result == true)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
