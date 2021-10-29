using DragonFly.AspNetCore.Identity.MongoDB.Services.Base;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB.Services
{
    class PasswordHashGenerator : IPasswordHashGenerator
    {
        public string Generate(string username, byte[] salt, string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                     password: password,
                                                                     salt: salt,
                                                                     prf: KeyDerivationPrf.HMACSHA512,
                                                                     iterationCount: 10_000,
                                                                     numBytesRequested: 512 / 8));

            return hashed;
        }

        public byte[] GenerateRandomSalt()
        {
            // Build the random bytes
            return RandomNumberGenerator.GetBytes(32);
        }
    }
}