// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly;

/// <summary>
/// IPrincipalContext
/// </summary>
public interface IPrincipalContext
{
    /// <summary>
    /// Principal
    /// </summary>
    ClaimsPrincipal? Principal { get; set; }
}
