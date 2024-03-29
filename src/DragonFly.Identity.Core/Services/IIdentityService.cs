﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Identity.Services;

public interface IIdentityService
{
    Task CreateUserAsync(IdentityUser user, string password);

    Task ChangePasswordAsync(Guid userId, string newPassword);

    Task<IdentityUser> GetUserAsync(string username);

    Task UpdateUserAsync(IdentityUser user);

    Task<IdentityUser> GetUserAsync(Guid id);

    Task<IdentityRole> GetRoleAsync(Guid id);

    Task UpdateRoleAsync(IdentityRole role);

    Task CreateRoleAsync(IdentityRole role);

    Task<IEnumerable<IdentityRole>> GetRolesAsync();

    Task<IEnumerable<IdentityUser>> GetUsersAsync();
}
