using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Builders
{
    /// <summary>
    /// DragonFlyBuilder
    /// </summary>
    public class DragonFlyBuilder : IDragonFlyBuilder
    {
        public DragonFlyBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Services
        /// </summary>
        public IServiceCollection Services { get; }

    }
}
