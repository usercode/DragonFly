// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DragonFly.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<DragonFly.App.Client.App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddDragonFlyClient()
            .AddRestApi()
            .AddBlockField()
            .AddIdentity()
            .AddApiKeys()            
            ;

WebAssemblyHost build = builder.Build();

await build.UseDragonFlyClient();
await build.RunAsync();
