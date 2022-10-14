// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Identity;

namespace DragonFly.Security;

/// <summary>
/// ILoginService
/// </summary>
public interface ILoginService
{
    Task<bool> LoginAsync(string username, string password, bool isPersistent);

    Task Logout();

    Task<IdentityUser?> GetCurrentUserAsync();
}
