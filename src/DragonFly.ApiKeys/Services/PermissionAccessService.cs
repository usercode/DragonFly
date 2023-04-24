// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.Permissions;
using DragonFLy.ApiKeys.AspNetCore.Storage.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DragonFLy;

internal class PermissionAccessService : IPermissionAccessService
{
    public PermissionAccessService(IDragonFlyApi api, MongoIdentityStore store)
    {
        Api = api;
        Store = store;
    }

    public IDragonFlyApi Api { get; }

    public MongoIdentityStore Store { get; }

    public async Task<bool> CanAccessAsync(ClaimsPrincipal principal, string permission)
    {
        Claim? claim = principal.FindFirst("ApiKeyId");

        if (claim == null)
        {
            return false;
        }

        Guid apikeyId = Guid.Parse(claim.Value);

        MongoApiKey apikey = await Store.ApiKeys.AsQueryable().FirstAsync(x => x.Id == apikeyId);

        IEnumerable<string> permissions = Api.Permissions().GetPolicy(permission);

        bool found = permissions.All(apikey.Permissions.Contains);

        return found;
    }
}
