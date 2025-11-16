// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DragonFly.AspNetCore.Identity.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using DragonFly.AspNetCore;

namespace DragonFly.ApiKeys;

class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    public PermissionHandler(IDragonFlyApi api, MongoIdentityStore store)
    {
        Api = api;
        Store = store;
    }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    /// <summary>
    /// Store
    /// </summary>
    private MongoIdentityStore Store { get; }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.HasSucceeded)
        {
            return;
        }

        Claim? claim = context.User.FindFirst("ApiKeyId");

        if (claim == null)
        {
            return;
        }

        Guid apikeyId = Guid.Parse(claim.Value);

        string[] permissions = Api.Permissions.Get(requirement.Permission);

        bool found = await Store.ApiKeys.AsQueryable()
                                            .Where(x => x.Id == apikeyId)
                                            .Where(x => permissions.Any(p => x.Permissions.Contains(p)))
                                            .AnyAsync()
                                            .ConfigureAwait(false);

        if (found)
        {
            context.Succeed(requirement);
        }
    }
}
