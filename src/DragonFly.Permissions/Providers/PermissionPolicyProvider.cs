// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DragonFly.Permissions;

class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = new AuthorizationPolicyBuilder();
        policy.AddRequirements(new PermissionRequirement(policyName));

        return Task.FromResult((AuthorizationPolicy?)policy.Build());
    }
}
