// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Identity;

/// <summary>
/// IdentityUser
/// </summary>
public class IdentityUser : Entity<IdentityUser>
{
    public IdentityUser()
    {
        Username = string.Empty;
        NormalizedUsername = string.Empty;
        Email = string.Empty;
        Roles = new List<IdentityRole>();
    }

    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// NormalizedUsername
    /// </summary>
    public string NormalizedUsername { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Roles
    /// </summary>
    public IList<IdentityRole> Roles { get; set; }
}
