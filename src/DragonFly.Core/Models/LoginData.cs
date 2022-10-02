// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Exports;

/// <summary>
/// LoginData
/// </summary>
public class LoginData
{
    public LoginData()
    {
        Username = String.Empty;
        Password = String.Empty;
        IsPersistent = false;
    }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool IsPersistent { get; set; }
}
