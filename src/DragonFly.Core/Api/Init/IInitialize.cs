// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// IInitialize
/// </summary>
public interface IInitialize
{
    Task ExecuteAsync(IDragonFlyApi api);
}
