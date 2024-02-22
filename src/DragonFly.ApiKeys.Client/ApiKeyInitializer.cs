// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.ApiKeys.Client.Components;
using DragonFly.Init;

namespace DragonFly.ApiKeys.Client;

public class ApiKeyInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {        
        api.Settings().Add<ApiKeyList>("ApiKey");

        return Task.CompletedTask;
    }
}
