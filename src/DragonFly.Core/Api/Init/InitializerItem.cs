// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Init;

class InitializerItem : IInitialize, IPreInitialize, IPostInitialize
{
    public InitializerItem(Action<IDragonFlyApi> action)
    {
        Action = action;
    }

    private Action<IDragonFlyApi> Action { get; }

    public Task ExecuteAsync(IDragonFlyApi api)
    {
        Action(api);

        return Task.CompletedTask;
    }
}
