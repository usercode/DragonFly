// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IPostInitialized
/// </summary>
public interface IPostInitialize
{
    Task ExecuteAsync(IDragonFlyApi api);
}
