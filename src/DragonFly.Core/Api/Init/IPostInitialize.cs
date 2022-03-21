using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// IPostInitialized
/// </summary>
public interface IPostInitialize
{
    Task ExecuteAsync(IDragonFlyApi api);
}
