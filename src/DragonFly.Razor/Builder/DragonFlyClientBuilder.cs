using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Builder
{
    public class DragonFlyClientBuilder : IDragonFlyClientBuilder
    {
        public DragonFlyClientBuilder(WebAssemblyHostBuilder webAssemblyHostBuilder)
        {
            WebAssemblyHostBuilder = webAssemblyHostBuilder;
        }

        /// <summary>
        /// WebAssemblyHostBuilder
        /// </summary>
        public WebAssemblyHostBuilder WebAssemblyHostBuilder { get; }
    }
}
