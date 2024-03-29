﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore.Exports;

/// <summary>
/// LoginData
/// </summary>
public class LoginData
{
    public LoginData()
    {
        Username = string.Empty;
        Password = string.Empty;
        IsPersistent = false;
    }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool IsPersistent { get; set; }
}
