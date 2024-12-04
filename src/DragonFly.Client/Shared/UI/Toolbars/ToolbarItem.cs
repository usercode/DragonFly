// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace DragonFly.Client;

public class ToolbarItem
{
    public ToolbarItem(string name, bool isAccent, Icon? icon, Func<Task> action)
    {
        Name = name;
        IsAccent = isAccent;
        Icon = icon;
        _action = action;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// IsAccent
    /// </summary>
    public bool IsAccent { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    public Icon? Icon { get; set; }

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

            await Task.Delay(TimeSpan.FromMilliseconds(400));
        }
        finally
        {
            IsRunning = false;
        }
    }
}
