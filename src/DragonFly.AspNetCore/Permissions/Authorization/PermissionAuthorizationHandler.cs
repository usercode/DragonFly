// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;

namespace DragonFly.Permissions;

class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    public PermissionAuthorizationHandler(IEnumerable<IPermissionAccessService> permissionServices)
    {
        PermissionServices = permissionServices;
    }

    private IEnumerable<IPermissionAccessService> PermissionServices { get; }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        bool result = false;

        foreach (IPermissionAccessService permission in PermissionServices)
        {
            result = await permission.CanAccessAsync(context.User, requirement.Permission);

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
