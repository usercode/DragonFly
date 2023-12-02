// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DragonFly.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<DragonFlyApp.Client.App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddDragonFly()
            .AddRestClient()
            .AddBlockField()
            .AddIdentity()
            .AddApiKeys();

WebAssemblyHost host = builder.Build();

await host.InitDragonFlyAsync();
await host.RunAsync();
