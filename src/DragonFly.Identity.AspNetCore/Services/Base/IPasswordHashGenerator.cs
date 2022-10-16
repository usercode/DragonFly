// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore.Identity.MongoDB.Services.Base;

public interface IPasswordHashGenerator
{
    byte[] Generate(string username, byte[] salt, string password);

    byte[] GenerateRandomSalt();
}
