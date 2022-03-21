using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;
using DragonFly.Contents.Content;

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
