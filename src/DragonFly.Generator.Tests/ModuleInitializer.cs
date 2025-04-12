// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Runtime.CompilerServices;

namespace DragonFly.Proxy.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        //VerifySourceGenerators.Initialize();
    }
}
