﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using DragonFly.AspNetCore;

namespace DragonFly.Identity;

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

        Claim? claim = context.User.FindFirst("UserId");

        if (claim == null)
        {
            return;
        }

        Guid userId = Guid.Parse(claim.Value);
        MongoIdentityUser user = await Store.Users.AsQueryable().FirstAsync(x => x.Id == userId).ConfigureAwait(false);

        string[] permissions = Api.Permission().Get(requirement.Permission);

        bool found = await Store.Roles.AsQueryable()
                                            .Where(x => user.Roles.Contains(x.Id))
                                            .Where(x => permissions.Any(p => x.Permissions.Contains(p)))
                                            .AnyAsync()
                                            .ConfigureAwait(false);

        if (found)
        {
            context.Succeed(requirement);
        }
    }
}
