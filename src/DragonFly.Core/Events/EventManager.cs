// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Events;

/// <summary>
/// EventManager
/// </summary>
public class EventManager
{
    public static EventManager? _default;

    /// <summary>
    /// Default
    /// </summary>
    public static EventManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new EventManager();
            }

            return _default;
        }
    }
}
