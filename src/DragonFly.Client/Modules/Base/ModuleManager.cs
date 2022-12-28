// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Collections.Generic;

namespace DragonFly.Client;

/// <summary>
/// ModuleManager
/// </summary>
public class ModuleManager
{
    private static ModuleManager _default;

    public static ModuleManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new ModuleManager();
            }

            return _default;
        }
    }

    public ModuleManager()
    {
        Modules = new List<ClientModule>();
    }

    public IList<ClientModule> Modules { get; }

    public void Add<TModule>()
        where TModule : ClientModule, new()
    {
        Modules.Add(new TModule());
    }
}
