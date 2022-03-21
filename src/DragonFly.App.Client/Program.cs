using DragonFly.ApiKeys.Razor;
using DragonFly.AspNetCore.Identity.Razor;
using DragonFly.Client.Core;
using DragonFly.Fields.BlockField.Razor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<DragonFly.App.Client.App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddDragonFlyClient()
            .AddBlockField()
            .AddIdentity()
            .AddApiKeys()
            ;

WebAssemblyHost build = builder.Build();

build.UseDragonFlyClient();

await build.RunAsync();
