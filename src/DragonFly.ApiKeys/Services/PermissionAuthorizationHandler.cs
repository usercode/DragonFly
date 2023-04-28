// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DragonFly.AspNetCore.Identity.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using DragonFLy.ApiKeys.AspNetCore.Storage.Models;
using DragonFly.Permissions;

namespace DragonFly.ApiKeys;

class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    public PermissionAuthorizationHandler(IDragonFlyApi api, MongoIdentityStore store)
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
        Claim? claim = context.User.FindFirst("ApiKeyId");

        if (claim == null)
        {
            return;
        }

        Guid apikeyId = Guid.Parse(claim.Value);

        MongoApiKey apikey = await Store.ApiKeys.AsQueryable().FirstAsync(x => x.Id == apikeyId);

        IEnumerable<string> permissions = Api.Permissions().GetPolicy(requirement.Permission);

        bool found = permissions.All(apikey.Permissions.Contains);

        if (found)
        {
            context.Succeed(requirement);
        }
    }
}
