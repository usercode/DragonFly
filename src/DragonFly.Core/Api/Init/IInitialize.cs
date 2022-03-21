using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// IInitialize
/// </summary>
public interface IInitialize
{
    Task ExecuteAsync(IDragonFlyApi api);
}
