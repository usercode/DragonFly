// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Identity;

namespace DragonFly.Security;

/// <summary>
/// ILoginService
/// </summary>
public interface ILoginService
{
    Task<LoginResult> LoginAsync(string username, string password, bool isPersistent);

    Task Logout();

    Task<IdentityUser?> GetCurrentUserAsync();
}
