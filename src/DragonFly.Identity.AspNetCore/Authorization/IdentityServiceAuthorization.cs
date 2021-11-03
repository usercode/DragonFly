using DragonFly.Identity.Permissions;
using DragonFly.Identity.Services;
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
            IPermissionService permissionService
            )
        {
            IdentityService = identityService;
            PermissionService = permissionService;
        }

        /// <summary>
        /// IdentityService
        /// </summary>
        public IIdentityService IdentityService { get; }

        /// <summary>
        /// PermissionService
        /// </summary>
        public IPermissionService PermissionService { get; }

        public async Task ChangePasswordAsync(Guid userId, string newPassword)
        {
            await PermissionService.AuthorizeAsync(IdentityPermissions.PasswordChange);

            await IdentityService.ChangePasswordAsync(userId, newPassword);
        }

        public async Task CreateRoleAsync(IdentityRole role)
        {
            await PermissionService.AuthorizeAsync(IdentityPermissions.RoleCreate);

            await IdentityService.CreateRoleAsync(role);
        }

        public async Task CreateUserAsync(IdentityUser user, string password)
        {
            await PermissionService.AuthorizeAsync(IdentityPermissions.UserCreate);

            await IdentityService.CreateUserAsync(user, password);
        }

        public async Task<IdentityRole> GetRoleAsync(Guid id)
        {
            await PermissionService.AuthorizeAsync(IdentityPermissions.RoleRead);

            return await IdentityService.GetRoleAsync(id);
        }

        public async Task<IEnumerable<IdentityRole>> GetRolesAsync()
        {
            await PermissionService.AuthorizeAsync(IdentityPermissions.RoleQuery);

            return await IdentityService.GetRolesAsync();
        }

        public async Task<IdentityUser> GetUserAsync(string username)
        {
            await PermissionService.AuthorizeAsync(IdentityPermissions.UserRead);

            return await IdentityService.GetUserAsync(username);
        }

        public async Task<IdentityUser> GetUserAsync(Guid id)
        {
            await PermissionService.AuthorizeAsync(IdentityPermissions.UserRead);

            return await IdentityService.GetUserAsync(id);
        }

        public async Task<IEnumerable<IdentityUser>> GetUsersAsync()
        {
            await PermissionService.AuthorizeAsync(IdentityPermissions.UserQuery);

            return await IdentityService.GetUsersAsync();
        }

        public async Task UpdateRoleAsync(IdentityRole role)
        {
            await PermissionService.AuthorizeAsync(IdentityPermissions.RoleUpdate);

            await IdentityService.UpdateRoleAsync(role);
        }

        public async Task UpdateUserAsync(IdentityUser user)
        {
            await PermissionService.AuthorizeAsync(IdentityPermissions.UserUpdate);

            await IdentityService.UpdateUserAsync(user);
        }
    }
}
