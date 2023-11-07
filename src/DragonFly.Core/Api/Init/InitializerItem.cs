// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Init;

class InitializerItem : IInitialize, IPreInitialize, IPostInitialize
{
    public InitializerItem(string name, Action<IDragonFlyApi> action)
    {
        Name = name;
        Action = action;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    private Action<IDragonFlyApi> Action { get; }

    public Task ExecuteAsync(IDragonFlyApi api)
    {
        Action(api);

        return Task.CompletedTask;
    }
}
