using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.AspNetCore.Identity.MongoDB.Services;
using DragonFly.AspNetCore.Identity.MongoDB.Services.Base;
using DragonFly.ContentItems;
using DragonFly.Core.Security;
using DragonFly.Identity;
using DragonFly.Identity.Permissions;
using DragonFly.Identity.Services;
using DragonFly.MongoDB.Options;
using DragonFly.Permissions;
using DragonFly.Permissions.AspNetCore.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB
{
    /// <summary>
    /// SeedDataAction
    /// </summary>
    class SeedDataAction : IPostInitialize
    {
        public SeedDataAction(
            IOptions<MongoDbOptions> options,
            MongoIdentityStore mongoStore)
        {
            Options = options.Value;
            Store = mongoStore;
        }

        public MongoDbOptions Options { get; }

        /// <summary>
        /// Store
        /// </summary>
        private MongoIdentityStore Store { get; }

        public async Task ExecuteAsync(IDragonFlyApi api)
        {
            if (string.IsNullOrEmpty(Options.InitialUsername)
                || string.IsNullOrEmpty(Options.InitialPassword))
            {
                return;
            }

            if (Store.Users.AsQueryable().Any())
            {
                return;
            }

            using (new DisablePermissionState())
            {
                IdentityRole roleAdmin = new IdentityRole();
                roleAdmin.Name = "Administrators";
                roleAdmin.Permissions.Add(ContentPermissions.ContentRead);
                roleAdmin.Permissions.Add(ContentPermissions.ContentQuery);
                roleAdmin.Permissions.Add(ContentPermissions.ContentCreate);
                roleAdmin.Permissions.Add(ContentPermissions.ContentUpdate);
                roleAdmin.Permissions.Add(ContentPermissions.ContentDelete);
                roleAdmin.Permissions.Add(ContentPermissions.ContentPublish);
                roleAdmin.Permissions.Add(ContentPermissions.ContentUnpublish);
                roleAdmin.Permissions.Add(AssetPermissions.AssetRead);
                roleAdmin.Permissions.Add(AssetPermissions.AssetQuery);
                roleAdmin.Permissions.Add(AssetPermissions.AssetCreate);
                roleAdmin.Permissions.Add(AssetPermissions.AssetUpdate);
                roleAdmin.Permissions.Add(AssetPermissions.AssetDelete);
                roleAdmin.Permissions.Add(AssetPermissions.AssetPublish);
                roleAdmin.Permissions.Add(AssetPermissions.AssetUnpublish);
                roleAdmin.Permissions.Add(AssetPermissions.AssetUpload);
                roleAdmin.Permissions.Add(AssetPermissions.AssetDownload);
                roleAdmin.Permissions.Add(IdentityPermissions.UserRead);
                roleAdmin.Permissions.Add(IdentityPermissions.UserQuery);
                roleAdmin.Permissions.Add(IdentityPermissions.UserCreate);
                roleAdmin.Permissions.Add(IdentityPermissions.UserUpdate);
                roleAdmin.Permissions.Add(IdentityPermissions.UserDelete);
                roleAdmin.Permissions.Add(IdentityPermissions.RoleRead);
                roleAdmin.Permissions.Add(IdentityPermissions.RoleQuery);
                roleAdmin.Permissions.Add(IdentityPermissions.RoleCreate);
                roleAdmin.Permissions.Add(IdentityPermissions.RoleUpdate);
                roleAdmin.Permissions.Add(IdentityPermissions.RoleDelete);

                await api.Identity().CreateRoleAsync(roleAdmin);

                IdentityRole roleReaders = new IdentityRole();
                roleReaders.Name = "ReadOnly";
                roleReaders.Permissions.Add(ContentPermissions.ContentRead);
                roleReaders.Permissions.Add(ContentPermissions.ContentQuery);
                roleReaders.Permissions.Add(AssetPermissions.AssetRead);
                roleReaders.Permissions.Add(AssetPermissions.AssetQuery);

                await api.Identity().CreateRoleAsync(roleReaders);

                IdentityUser admin = new IdentityUser();
                admin.Username = Options.InitialUsername;
                admin.Email = DefaultSecurity.DefaultEmail;
                admin.Roles.Add(roleAdmin);

                await api.Identity().CreateUserAsync(admin, Options.InitialPassword);
            }
        }                  
    }
}
