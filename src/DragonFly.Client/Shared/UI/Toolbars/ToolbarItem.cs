// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using System;
using System.Threading.Tasks;

namespace DragonFly.Client;

public class ToolbarItem
{
    public ToolbarItem(string name, BSColor color, Func<Task> action)
    {
        Name = name;
        Color = color;
        _action = action;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Color
    /// </summary>
    public BSColor Color { get; set; }

    /// <summary>
    /// IsRunning
    /// </summary>
    public bool IsRunning { get; private set; }

    private Func<Task> _action;

    /// <summary>
    /// ExecuteAsync
    /// </summary>
    /// <returns></returns>
    public async Task ExecuteAsync()
    {
        try
        {
            IsRunning = true;

            await _action();
        }
        finally
        {
            IsRunning = false;
        }
    }
}
