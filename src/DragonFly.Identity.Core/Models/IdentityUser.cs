// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Identity;

/// <summary>
/// IdentityUser
/// </summary>
public class IdentityUser : Entity<IdentityUser>
{
    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// NormalizedUsername
    /// </summary>
    public string NormalizedUsername { get; set; } = string.Empty;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Roles
    /// </summary>
    public IList<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
}
