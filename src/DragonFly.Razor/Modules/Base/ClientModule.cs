using DragonFly.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Modules
{
    public abstract class ClientModule : IDragonFlyModule
    {
        public ClientModule()
        {
            Version = GetType().Assembly.GetName().Version;
        }

        public abstract string Name { get; }

        public virtual string Description { get; }

        public virtual string Author { get; }

        public virtual Version Version { get; } 

        public virtual void Init(IDragonFlyApi api)
        {

        }
    }
}
