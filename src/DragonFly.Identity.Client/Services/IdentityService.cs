// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Identity.Commands;
using DragonFly.Identity.Rest.Commands;
using DragonFly.Identity.Services;
using System.Net.Http.Json;

namespace DragonFly.Identity.Razor.Services;

/// <summary>
/// IdentityService
/// </summary>
class IdentityService : IIdentityService
{
    public IdentityService(HttpClient client)
    {
        Client = client;
    }

    /// <summary>
    /// Client
    /// </summary>
    public HttpClient Client { get; }

    public async Task ChangePasswordAsync(Guid id, string newPassword)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync("api/identity/user/change-password", new ChangePassword() { UserId = id, NewPassword = newPassword }, ApiJsonSerializerContext.Default.ChangePassword);

        response.EnsureSuccessStatusCode();
    }

    public async Task CreateRoleAsync(IdentityRole role)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync("api/identity/role", role, IdentitySerializerContext.Default.IdentityRole);

        response.EnsureSuccessStatusCode();
    }

    public async Task CreateUserAsync(IdentityUser user, string password)
    {
        CreateUser createUser = new CreateUser();
        createUser.User = user;
        createUser.Password = password;

        HttpResponseMessage response = await Client.PostAsJsonAsync("api/identity/user", createUser, IdentitySerializerContext.Default.CreateUser);

        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<IdentityRole>> GetRolesAsync()
    {
        HttpResponseMessage response = await Client.PostAsync($"api/identity/role/query", new StringContent(""));

        IEnumerable<IdentityRole>? result = await response.Content.ReadFromJsonAsync(IdentitySerializerContext.Default.IEnumerableIdentityRole);

        if (result == null)
        {
            throw new Exception();
        }

        return result;
    }

    public async Task<IEnumerable<IdentityUser>> GetUsersAsync()
    {
        HttpResponseMessage response = await Client.PostAsync($"api/identity/user/query", new StringContent(""));

        IEnumerable<IdentityUser>? result = await response.Content.ReadFromJsonAsync(IdentitySerializerContext.Default.IEnumerableIdentityUser);

        if (result == null)
        {
            throw new Exception();
        }

        return result;
    }

    public async Task<IdentityUser> GetUserAsync(string username)
    {
        IdentityUser? user = await Client.GetFromJsonAsync($"api/identity/user/{username}", IdentitySerializerContext.Default.IdentityUser);

        if (user == null)
        {
            throw new Exception();
        }

        return user;
    }

    public async Task<IdentityUser> GetUserAsync(Guid id)
    {
        IdentityUser? user = await Client.GetFromJsonAsync($"api/identity/user/{id}", IdentitySerializerContext.Default.IdentityUser);

        if (user == null)
        {
            throw new Exception();
        }

        return user;
    }

    public async Task UpdateUserAsync(IdentityUser user)
    {
        HttpResponseMessage response = await Client.PutAsJsonAsync("api/identity/user", user, IdentitySerializerContext.Default.IdentityUser);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateRoleAsync(IdentityRole role)
    {
        HttpResponseMessage response = await Client.PutAsJsonAsync("api/identity/role", role, IdentitySerializerContext.Default.IdentityRole);

        response.EnsureSuccessStatusCode();
    }

    public async Task<IdentityRole> GetRoleAsync(Guid id)
    {
        IdentityRole? role = await Client.GetFromJsonAsync($"api/identity/role/{id}", IdentitySerializerContext.Default.IdentityRole);

        if (role == null)
        {
            throw new Exception();
        }

        return role;
    }
}
