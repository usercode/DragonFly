// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

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
