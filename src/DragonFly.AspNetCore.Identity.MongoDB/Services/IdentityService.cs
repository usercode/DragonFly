using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using DragonFly.AspNetCore.Identity.MongoDB.Services.Base;
using DragonFly.AspNetCore.Identity.MongoDB.Storages.Models;
using DragonFly.Security;
using DragonFly.Identity;

namespace DragonFly.AspNetCore.Identity.MongoDB.Services
{
    /// <summary>
    /// IdentityService
    /// </summary>
    class IdentityService : IIdentityService, ILoginService
    {
        public IdentityService(
            MongoIdentityStore store,
            IHttpContextAccessor httpContextAccessor,
            IPasswordHashGenerator passwordGenerater)
        {
            Store = store;
            HttpContext = httpContextAccessor;
            PasswordHashGenerator = passwordGenerater;
        }

        /// <summary>
        /// Store
        /// </summary>
        public MongoIdentityStore Store { get; }

        /// <summary>
        /// PasswordGenerater
        /// </summary>
        public IPasswordHashGenerator PasswordHashGenerator { get; }

        /// <summary>
        /// HttpContext
        /// </summary>
        public IHttpContextAccessor HttpContext { get; }

        public async Task<bool> LoginAsync(string username, string password, bool isPersistent)
        {
            MongoIdentityUser user = await Store.Users.AsQueryable().FirstOrDefaultAsync(x => x.Username == username);

            string hashed = PasswordHashGenerator.Generate(user.Username, Convert.FromBase64String(user.Salt), password);

            if (user.Password != hashed)
            {
                return false;
            }

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserId", user.Id.ToString()));
            claims.Add(new Claim("Username", user.Username));

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Password"));

            await HttpContext.HttpContext!.SignInAsync(principal, new AuthenticationProperties() { IsPersistent = isPersistent });

            return true;
        }

        public async Task Logout()
        {
            await HttpContext.HttpContext!.SignOutAsync();
        }

        public async Task CreateUserAsync(IdentityUser user, string password)
        {
            MongoIdentityUser mongoIdentityUser = user.ToMongo();

            mongoIdentityUser.NormalizedUsername = mongoIdentityUser.Username.ToUpper();
            mongoIdentityUser.NormalizedEmail = mongoIdentityUser.Email.ToUpper();

            await Store.Users.InsertOneAsync(mongoIdentityUser);

            user.Id = mongoIdentityUser.Id;

            await ChangePasswordAsync(mongoIdentityUser, password);
        }

        public async Task ChangePasswordAsync(Guid id, string newPassword)
        {
            MongoIdentityUser user = await Store.Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

            await ChangePasswordAsync(user, newPassword);
        }

        public async Task ChangePasswordAsync(MongoIdentityUser user, string newPassword)
        {
            byte[] salt = PasswordHashGenerator.GenerateRandomSalt();
            string password = PasswordHashGenerator.Generate(user.Username, salt, newPassword);

            string saltAsBase64 = Convert.ToBase64String(salt);

            await Store.Users.UpdateOneAsync(
                                Builders<MongoIdentityUser>.Filter.Eq(x => x.Id, user.Id),
                                Builders<MongoIdentityUser>.Update
                                                    .Set(x => x.Password, password)
                                                    .Set(x => x.Salt, saltAsBase64));
        }

        public async Task DeleteUserAsync(IdentityUser user)
        {
            await Store.Users.DeleteOneAsync(Builders<MongoIdentityUser>.Filter.Eq(x => x.Id, user.Id));
        }

        public async Task<IdentityUser> GetUserAsync(string username)
        {
            username = username.ToUpper();

            MongoIdentityUser user = await Store.Users
                                                    .AsQueryable()
                                                    .FirstOrDefaultAsync(x => x.NormalizedUsername == username);

            return user.ToModel(Store);
        }

        public async Task<IdentityUser> GetUserAsync(Guid id)
        {
            MongoIdentityUser user = await Store.Users
                                                    .AsQueryable()
                                                    .FirstOrDefaultAsync(x => x.Id == id);

            return user.ToModel(Store);
        }

        public async Task<IEnumerable<IdentityUser>> GetUsersAsync()
        {
            var items = await Store.Users
                                        .AsQueryable()
                                        .ToListAsync();

            return items.Select(x => x.ToModel(Store)).ToList();
        }

        public async Task CreateRoleAsync(IdentityRole role)
        {
            MongoIdentityRole identity = role.ToMongo();

            await Store.Roles.InsertOneAsync(identity);

            role.Id = identity.Id;
        }

        public async Task<IEnumerable<IdentityRole>> GetRolesAsync()
        {
            var items = await Store.Roles.AsQueryable().ToListAsync();

            return items.Select(x => x.ToModel()).ToList();
        }

        public async Task<IEnumerable<IdentityPermission>> GetPermissionsAsync()
        {
            throw new Exception();
            //return await Store.Permissions.AsQueryable().ToListAsync();
        }

        public async Task UpdateUserAsync(IdentityUser user)
        {
            await Store.Users.UpdateOneAsync(Builders<MongoIdentityUser>.Filter.Eq(x => x.Id, user.Id),
                                            Builders<MongoIdentityUser>.Update
                                                                        .Set(x => x.Username, user.UserName)
                                                                        .Set(x=> x.Roles, user.Roles.Select(r => r.Id).ToList())                                                                        
                                                                        );
        }

        public async Task UpdateRoleAsync(IdentityRole role)
        {
            await Store.Roles.UpdateOneAsync(Builders<MongoIdentityRole>.Filter.Eq(x => x.Id, role.Id),
                                            Builders<MongoIdentityRole>.Update
                                                                        .Set(x => x.Name, role.Name)
                                                                        );
        }

        public async Task<IdentityRole> GetRoleAsync(Guid id)
        {
            MongoIdentityRole role = await Store.Roles.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

            return role.ToModel();
        }
    }
}
