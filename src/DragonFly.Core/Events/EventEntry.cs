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
/// EventEntry
/// </summary>
public class EventEntry : ContentBase
{
    public EventEntry(string name, string type)
    {
        Name = name;
        Type = type;
    }

    /// <summary>
    /// Date
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    public object? Data { get; set; }
}
