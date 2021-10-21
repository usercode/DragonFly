using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DragonFly.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using DragonFly.Client.Core;
using DragonFly.App.Client;
using DragonFly.Client.Core.Assets;
using DragonFly.Fields.BlockField.Razor;
using DragonFly.AspNetCore.Identity.Razor;

namespace DragonFly.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<DragonFly.App.Client.App>("app");
            //builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.AddDragonFlyClient()
                        .AddBlockField()
                        .AddIdentity()
                        ;

            WebAssemblyHost build = builder.Build();

            build.UseDragonFLyClient();

            await build.RunAsync();
        }
    }
}
 