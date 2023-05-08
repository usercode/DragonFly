// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace DragonFly.AspNetCore;

/// <summary>
/// PermissionPolicyProvider
/// </summary>
public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        DefaultProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    /// <summary>
    /// DefaultProvider
    /// </summary>
    private DefaultAuthorizationPolicyProvider DefaultProvider { get; }

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        //DragonFly permission?
        if (policyName.StartsWith(Permission.PolicyPrefix))
        {
            //remove prefix
            string permission = policyName[Permission.PolicyPrefix.Length..];

            //build policy
            var policy = new AuthorizationPolicyBuilder(PermissionSchemeManager.GetAll());
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement(permission));

            return Task.FromResult((AuthorizationPolicy?)policy.Build());
        }
        else //use default policy provider
        {
            return DefaultProvider.GetPolicyAsync(policyName);
        }
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => DefaultProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => DefaultProvider.GetFallbackPolicyAsync();
}
