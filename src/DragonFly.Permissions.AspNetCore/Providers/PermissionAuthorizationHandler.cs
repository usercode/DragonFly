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
        public PermissionAuthorizationHandler(IAuthorizePermissionService permissionService)
        {
            PermissionService = permissionService;
        }

        private IAuthorizePermissionService PermissionService { get; }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            bool result = await PermissionService.AuthorizeAsync(context.User, requirement.Permission);

            if (result)
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
