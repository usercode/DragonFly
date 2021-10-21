using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    /// <summary>
    /// IPreInitialize
    /// </summary>
    public interface IPreInitialize
    {
        Task ExecuteAsync(IDragonFlyApi api);
    }
}
