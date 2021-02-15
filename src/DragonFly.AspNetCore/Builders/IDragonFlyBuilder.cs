using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Builders
{
    public interface IDragonFlyBuilder
    {
        IServiceCollection Services { get; }
    }
}
