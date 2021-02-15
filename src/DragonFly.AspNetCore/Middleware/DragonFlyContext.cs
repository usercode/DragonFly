using DragonFly.AspNet.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Middleware
{
    public class DragonFlyContext
    {
        public DragonFlyContext(IOptions<DragonFlyOptions> options)
        {
            Options = options.Value;
        }

        public DragonFlyOptions Options { get; }

    }
}
