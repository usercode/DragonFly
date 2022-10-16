// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Identity;

/// <summary>
/// IdentityRole
/// </summary>
public class IdentityRole : Entity
{
    public IdentityRole()
    {
        Name = string.Empty;
        Permissions = new List<string>();
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Permissions
    /// </summary>
    public IList<string> Permissions { get; set; }
}
