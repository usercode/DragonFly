using DragonFly.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoDbOptions
/// </summary>
public class MongoDbOptions
{
    public MongoDbOptions()
    {
        Database = "DragonFly_App";
        Hostname = "localhost";
        Port = 27017;
        Username = "root";
        Password = "";
        InitialUsername = DefaultSecurity.DefaultUsername;
        InitialPassword = DefaultSecurity.DefaultPassword;
    }

    /// <summary>
    /// Database
    /// </summary>
    public string Database { get; set; }

    /// <summary>
    /// Hostname
    /// </summary>
    public string Hostname { get; set; }

    /// <summary>
    /// Port
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// InitialUsername
    /// </summary>
    public string InitialUsername { get; set; }

    /// <summary>
    /// InitialPassword
    /// </summary>
    public string InitialPassword { get; set; }
}
