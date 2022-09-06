using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// IDragonFlyModule
/// </summary>
public interface IDragonFlyModule
{
    /// <summary>
    /// Name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Author
    /// </summary>
    string Author { get; }

    /// <summary>
    /// Version
    /// </summary>
    Version Version { get; }


}
