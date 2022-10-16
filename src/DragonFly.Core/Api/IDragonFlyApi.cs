﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IDragonFlyApi
{
    Task InitAsync();

    IServiceProvider ServiceProvider { get; }
}
