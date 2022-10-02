// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Security;

/// <summary>
/// ILoginService
/// </summary>
public interface ILoginService
{
    Task<bool> LoginAsync(string username, string password, bool isPersistent);

    Task Logout();
}
