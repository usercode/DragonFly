using DragonFly.Content;
using DragonFly.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public class DragonFlyApi : IDragonFlyApi
    {
        public DragonFlyApi(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        
        public IServiceProvider ServiceProvider { get; }

        public async Task InitAsync()
        {
            foreach (IPreInitialize item in ServiceProvider.GetServices<IPreInitialize>())
            {
                await item.ExecuteAsync(this);
            }

            foreach (IInitialize item in ServiceProvider.GetServices<IInitialize>())
            {
                await item.ExecuteAsync(this);
            }

            foreach (IPostInitialize item in ServiceProvider.GetServices<IPostInitialize>())
            {
                await item.ExecuteAsync(this);
            }
        }
    }
}
