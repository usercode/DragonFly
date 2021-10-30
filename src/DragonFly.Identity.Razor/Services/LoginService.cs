using DragonFly.AspNetCore.Exports;
using DragonFly.Client;
using DragonFly.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.Razor
{
    class LoginService : ILoginService
    {
        public LoginService(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public async Task<bool> LoginAsync(string username, string password, bool isPersistent)
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync("login", new LoginData() { Username = username, Password = password, IsPersistent = isPersistent });

            return response.IsSuccessStatusCode;
        }

        public async Task Logout()
        {
            await Client.PostAsync("Logout", new StringContent(string.Empty));
        }
    }
}
