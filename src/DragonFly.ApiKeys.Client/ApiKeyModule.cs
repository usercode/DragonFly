// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.ApiKeys.Client.Components;
using DragonFly.Client;

namespace DragonFly.ApiKeys.Client;

public class ApiKeyModule : ClientModule
{
    public override string Name => "ApiKey";

    public override string Author => "DragonFly";

    public override void Init(IDragonFlyApi api)
    {
        api.Settings().Add<ApiKeyList>("ApiKey");
    }
}
