// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Exports;
using DragonFly.Identity;
using DragonFly.Security;
using System.Net.Http.Json;

namespace DragonFly.AspNetCore.Identity.Razor;

class LoginService : ILoginService
{
    public LoginService(HttpClient client)
    {
        Client = client;
    }

    private HttpClient Client { get; }

    public async Task<bool> LoginAsync(string username, string password, bool isPersistent)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync("api/identity/login", new LoginData() { Username = username, Password = password, IsPersistent = isPersistent }, ApiJsonSerializerContext.Default.LoginData);

        return response.IsSuccessStatusCode;
    }

    public async Task Logout()
    {
        await Client.PostAsync("Logout", new StringContent(string.Empty));
    }

    public async Task<IdentityUser?> GetCurrentUserAsync()
    {
        HttpResponseMessage response = await Client.GetAsync("api/identity/CurrentUser");

        if (response.IsSuccessStatusCode == false)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync(ApiJsonSerializerContext.Default.IdentityUser);
    }
}
