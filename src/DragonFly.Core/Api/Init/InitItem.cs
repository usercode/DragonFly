using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

class InitItem : IInitialize, IPreInitialize, IPostInitialize
{
    public InitItem(Action<IDragonFlyApi> action)
    {
        Action = action;
    }

    private Action<IDragonFlyApi> Action { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        Action(api);
    }
}
