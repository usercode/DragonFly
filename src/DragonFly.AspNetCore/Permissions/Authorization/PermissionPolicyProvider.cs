// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace DragonFly.Permissions;

class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() //=> FallbackPolicyProvider.GetDefaultPolicyAsync();
    {
        var policy = new AuthorizationPolicyBuilder("DragonFly_Identity", "DragonFly_ApiKey");
        policy.RequireAssertion(x => true);

        return Task.FromResult(policy.Build());
    }

public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = new AuthorizationPolicyBuilder("DragonFly_Identity", "DragonFly_ApiKey");
        policy.AddRequirements(new PermissionRequirement(policyName));        

        return Task.FromResult((AuthorizationPolicy?)policy.Build());
    }
}
