using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore
{
    public interface IDragonFlyApi
    {
        void Init();

        ContentFieldManager Fields { get; }
    }
}
