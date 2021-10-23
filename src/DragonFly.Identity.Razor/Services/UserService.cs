using DragonFly.Identity.Models;
using DragonFly.Identity.Razor.Models;
using DragonFly.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Razor.Services
{
    /// <summary>
    /// UserService
    /// </summary>
    internal class UserService : IUserStore
    {
        public UserService(HttpClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Client
        /// </summary>
        public HttpClient Client { get; }

        public async Task<IDragonFlyUser> GetUserAsync(string username)
        {
            throw new Exception();
        }

        public async Task<IList<IDragonFlyUser>> GetUsersAsync()
        {
            var response = await Client.PostAsJsonAsync($"identity/user", new StringContent(""));

            IList<DragonFlyUser>? result = await response.Content.ReadFromJsonAsync<IList<DragonFlyUser>>();

            if (result == null)
            {
                throw new Exception();
            }

            return result.Cast<IDragonFlyUser>().ToList();
        }
    }
}
