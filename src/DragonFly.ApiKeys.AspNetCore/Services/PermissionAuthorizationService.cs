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

namespace DragonFLy.ApiKeys.AspNetCore.Services;

internal class PermissionAuthorizationService : IPermissionAuthorizationService
{
    public PermissionAuthorizationService(IDragonFlyApi api, MongoIdentityStore store)
    {
        Api = api;
        Store = store;
    }

    public IDragonFlyApi Api { get; }

    public MongoIdentityStore Store { get; }

    public async Task<bool> AuthorizeAsync(ClaimsPrincipal principal, string permission)
    {
        using (new DisablePermissions())
        {
            Claim? claim = principal.FindFirst("ApiKeyId");

            if (claim == null)
            {
                return false;
            }

            Guid apikeyId = Guid.Parse(claim.Value);

            MongoApiKey apikey = await Store.ApiKeys.AsQueryable().FirstAsync(x => x.Id == apikeyId);

            IEnumerable<string> permissions = Api.Permission().GetPolicy(permission);

            bool found = permissions.All(p => apikey.Permissions.Contains(p));

            return found;
        }
    }
}
