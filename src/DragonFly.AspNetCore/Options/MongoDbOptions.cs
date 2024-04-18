// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Security;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoDbOptions
/// </summary>
public class MongoDbOptions
{

    /// <summary>
    /// Database
    /// </summary>
    public string Database { get; set; } = "DragonFly_App";

    /// <summary>
    /// Hostname
    /// </summary>
    public string Hostname { get; set; } = "localhost";

    /// <summary>
    /// Port
    /// </summary>
    public int Port { get; set; } = 27017;

    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// InitialUsername
    /// </summary>
    public string InitialUsername { get; set; } = DefaultSecurity.DefaultUsername;

    /// <summary>
    /// InitialPassword
    /// </summary>
    public string InitialPassword { get; set; } = DefaultSecurity.DefaultPassword;
}
