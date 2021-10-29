using DragonFly.Content;
using DragonFly.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    /// <summary>
    /// DragonFlyApi
    /// </summary>
    public class DragonFlyApi : IDragonFlyApi
    {
        public DragonFlyApi(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        
        public IServiceProvider ServiceProvider { get; }

        public async Task InitAsync()
        {
            using IServiceScope scope = ServiceProvider.CreateScope();

            foreach (IPreInitialize item in scope.ServiceProvider.GetServices<IPreInitialize>())
            {
                await item.ExecuteAsync(this);
            }

            foreach (IInitialize item in scope.ServiceProvider.GetServices<IInitialize>())
            {
                await item.ExecuteAsync(this);
            }

            foreach (IPostInitialize item in scope.ServiceProvider.GetServices<IPostInitialize>())
            {
                await item.ExecuteAsync(this);
            }
        }
    }
}
