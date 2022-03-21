﻿using DragonFly.AspNetCore.Identity.MongoDB.Services.Base;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB.Services;

class PasswordHashGenerator : IPasswordHashGenerator
{
    public byte[] Generate(string username, byte[] salt, string password)
    {
        byte[] hash = KeyDerivation.Pbkdf2(
                                            password: password,
                                            salt: salt,
                                            prf: KeyDerivationPrf.HMACSHA512,
                                            iterationCount: 10_000,
                                            numBytesRequested: 512 / 8);

        return hash;
    }

    public byte[] GenerateRandomSalt()
    {
        // Build the random bytes
        return RandomNumberGenerator.GetBytes(32);
    }
}
