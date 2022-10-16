// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;

namespace DragonFly.Models;

/// <summary>
/// RestContentEmbedded
/// </summary>
public class RestContentEmbedded
{
    public RestContentEmbedded()
    {
        Fields = new RestContentFields();
    }

    /// <summary>
    /// Schema
    /// </summary>
    public RestContentSchema Schema { get; set; }

    /// <summary>
    /// Fields
    /// </summary>
    public RestContentFields Fields { get; set; }
}
