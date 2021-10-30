﻿using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Identity.Commands;
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
            HttpResponseMessage response = await Client.PostAsJsonAsync("identity/user/change-password", new ChangePassword() { UserId = id, NewPassword = newPassword });

            response.EnsureSuccessStatusCode();
        }

        public async Task CreateRoleAsync(IdentityRole role)
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync("identity/role", role);

            response.EnsureSuccessStatusCode();
        }

        public async Task CreateUserAsync(IdentityUser user, string password)
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync("identity/user", user);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<IdentityPermission>> GetPermissionsAsync()
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync($"identity/permission/query", new StringContent(""));

            IList<IdentityPermission>? result = await response.Content.ReadFromJsonAsync<IList<IdentityPermission>>();

            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task<IEnumerable<IdentityRole>> GetRolesAsync()
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync($"identity/role/query", new StringContent(""));

            IList<IdentityRole>? result = await response.Content.ReadFromJsonAsync<IList<IdentityRole>>();

            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task<IEnumerable<IdentityUser>> GetUsersAsync()
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync($"identity/user/query", new StringContent(""));

            IList<IdentityUser>? result = await response.Content.ReadFromJsonAsync<IList<IdentityUser>>();

            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task<IdentityUser> GetUserAsync(string username)
        {
            IdentityUser? user = await Client.GetFromJsonAsync<IdentityUser>($"identity/user/{username}");

            if (user == null)
            {
                throw new Exception();
            }

            return user;
        }

        public async Task<IdentityUser> GetUserAsync(Guid id)
        {
            IdentityUser? user = await Client.GetFromJsonAsync<IdentityUser>($"identity/user/{id}");

            if (user == null)
            {
                throw new Exception();
            }

            return user;
        }

        public async Task UpdateUserAsync(IdentityUser user)
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync("identity/user", user);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateRoleAsync(IdentityRole role)
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync("identity/role", role);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IdentityRole> GetRoleAsync(Guid id)
        {
            IdentityRole? role = await Client.GetFromJsonAsync<IdentityRole>($"identity/role/{id}");

            if (role == null)
            {
                throw new Exception();
            }

            return role;
        }
    }
}