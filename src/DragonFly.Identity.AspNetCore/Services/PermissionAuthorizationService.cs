using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Permissions;
using DragonFly.Permissions.AspNetCore;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.AspNetCore.Permissions
{
    /// <summary>
    /// PermissionService
    /// </summary>
    class PermissionAuthorizationService : IPermissionAuthorizationService
    {
        public PermissionAuthorizationService(
            MongoIdentityStore store,
            IDragonFlyApi api)
        {
            Store = store;
            Api = api;
        }

        /// <summary>
        /// Store
        /// </summary>
        public MongoIdentityStore Store { get; }

        /// <summary>
        /// Api
        /// </summary>
        public IDragonFlyApi Api { get; }

        public async Task<bool> AuthorizeAsync(ClaimsPrincipal principal, string permission)
        {
            Claim? claim = principal.FindFirst("UserId");

            if (claim == null)
            {
                throw new Exception("Unknown UserID");
            }

            Guid userId = Guid.Parse(claim.Value);

            MongoIdentityUser user = await Store.Users.AsQueryable().FirstAsync(x => x.Id == userId);

            IEnumerable<string> permissions = Api.Permission().GetPolicy(permission);

            bool found = await Store.Roles.AsQueryable().AnyAsync(x => user.Roles.Contains(x.Id) && permissions.All(p => x.Permissions.Contains(p)));

            return found;
        }
    }
}
