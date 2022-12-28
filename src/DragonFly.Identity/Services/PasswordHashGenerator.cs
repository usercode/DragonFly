// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB.Services.Base;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

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
