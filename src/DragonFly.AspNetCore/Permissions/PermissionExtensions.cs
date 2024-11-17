// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using Microsoft.AspNetCore.Authorization;

namespace DragonFly;

public static class PermissionExtensions
{
    public static AuthorizationPolicy ToAuthorizationPolicy(this Permission permission)
    {
        var policy = new AuthorizationPolicyBuilder(PermissionConstants.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.AddRequirements(new PermissionRequirement(permission.Name));

        return policy.Build();
    }
}
