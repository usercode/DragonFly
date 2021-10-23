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
    public class IdentityLoginService : ILoginService
    {
        public IdentityLoginService(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public async Task<bool> LoginAsync(string username, string password, bool isPersistent)
        {
            var response = await Client.PostAsJsonAsync("login", new LoginData() { Username = username, Password = password, IsPersistent = false });

            return response.IsSuccessStatusCode;
        }

        public async Task Logout()
        {
            await Client.PostAsync("Logout", new StringContent(string.Empty));
        }
    }
}
