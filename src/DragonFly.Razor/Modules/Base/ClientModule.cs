// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;

namespace DragonFly.Razor.Modules;

public abstract class ClientModule : IDragonFlyModule
{
    public ClientModule()
    {
        Version = GetType().Assembly.GetName().Version;
    }

    public abstract string Name { get; }

    public virtual string Description { get; }

    public virtual string Author { get; }

    public virtual Version Version { get; } 

    public virtual void Init(IDragonFlyApi api)
    {

    }
}
