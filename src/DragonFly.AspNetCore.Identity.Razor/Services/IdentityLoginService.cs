﻿using DragonFly.AspNetCore.Exports;
using DragonFly.Client;
using DragonFly.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<bool> LoginAsync(string username, string password)
        {
            var response = await Client.PostAsJson("Login", new LoginData() { Username = username, Password = password });

            return response.IsSuccessStatusCode;
        }

        public async Task Logout()
        {
            await Client.PostAsync("Logout", new StringContent(string.Empty));
        }
    }
}
