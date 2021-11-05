using DragonFly.Identity.Permissions;
using DragonFly.Identity.Services;
using DragonFly.Permissions;
using DragonFly.Permissions.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.AspNetCore.Authorization
{
    /// <summary>
    /// IdentityStorageAuthorization
    /// </summary>
    class IdentityServiceAuthorization : IIdentityService
    {
        public IdentityServiceAuthorization(
            IIdentityService identityService,
            IDragonFlyApi api
            )
        {
            IdentityService = identityService;
            Api = api;
        }

        /// <summary>
        /// IdentityService
        /// </summary>
        public IIdentityService IdentityService { get; }

        /// <summary>
        /// PermissionService
        /// </summary>
        public IDragonFlyApi Api { get; }

        public async Task ChangePasswordAsync(Guid userId, string newPassword)
        {
            await Api.AuthorizeAsync(IdentityPermissions.PasswordChange);

            await IdentityService.ChangePasswordAsync(userId, newPassword);
        }

        public async Task CreateRoleAsync(IdentityRole role)
        {
            await Api.AuthorizeAsync(IdentityPermissions.RoleCreate);

            await IdentityService.CreateRoleAsync(role);
        }

        public async Task CreateUserAsync(IdentityUser user, string password)
        {
            await Api.AuthorizeAsync(IdentityPermissions.UserCreate);

            await IdentityService.CreateUserAsync(user, password);
        }

        public async Task<IdentityRole> GetRoleAsync(Guid id)
        {
            await Api.AuthorizeAsync(IdentityPermissions.RoleRead);

            return await IdentityService.GetRoleAsync(id);
        }

        public async Task<IEnumerable<IdentityRole>> GetRolesAsync()
        {
            await Api.AuthorizeAsync(IdentityPermissions.RoleQuery);

            return await IdentityService.GetRolesAsync();
        }

        public async Task<IdentityUser> GetUserAsync(string username)
        {
            await Api.AuthorizeAsync(IdentityPermissions.UserRead);

            return await IdentityService.GetUserAsync(username);
        }

        public async Task<IdentityUser> GetUserAsync(Guid id)
        {
            await Api.AuthorizeAsync(IdentityPermissions.UserRead);

            return await IdentityService.GetUserAsync(id);
        }

        public async Task<IEnumerable<IdentityUser>> GetUsersAsync()
        {
            await Api.AuthorizeAsync(IdentityPermissions.UserQuery);

            return await IdentityService.GetUsersAsync();
        }

        public async Task UpdateRoleAsync(IdentityRole role)
        {
            await Api.AuthorizeAsync(IdentityPermissions.RoleUpdate);

            await IdentityService.UpdateRoleAsync(role);
        }

        public async Task UpdateUserAsync(IdentityUser user)
        {
            await Api.AuthorizeAsync(IdentityPermissions.UserUpdate);

            await IdentityService.UpdateUserAsync(user);
        }
    }
}
