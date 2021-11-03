using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.AspNetCore.Identity.MongoDB.Models;
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
    class PermissionService : IPermissionService
    {
        public PermissionService(
            MongoIdentityStore store,
            IDragonFlyApi api,
            IHttpContextAccessor httpContextAccessor)
        {
            Store = store;
            Api = api;
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Store
        /// </summary>
        public MongoIdentityStore Store { get; }

        /// <summary>
        /// Api
        /// </summary>
        public IDragonFlyApi Api { get; }

        /// <summary>
        /// HttpContextAccessor
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; }

        public async Task AuthorizeAsync(string permission)
        {
            IEnumerable<string> permissions = Api.Permission().GetPolicy(permission);

            if (HttpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated == false)
            {
                throw new Exception("The current user is not authenticated.");
            }

            Claim? claim = HttpContextAccessor.HttpContext!.User.FindFirst("UserId");

            if (claim == null)
            {
                throw new Exception("Unknown UserID");
            }

            Guid userId = Guid.Parse(claim.Value);

            MongoIdentityUser user = await Store.Users.AsQueryable().FirstAsync(x => x.Id == userId);

            bool found = await Store.Roles.AsQueryable().AnyAsync(x => user.Roles.Contains(x.Id) && permissions.All(p => x.Permissions.Contains(p)));

            if (found == false)
            {
                throw new Exception($"Access denied: {permission}");
            }
        }
    }
}
