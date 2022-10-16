// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Contents.Content;

namespace DragonFly.AspNetCore.API.Models;

/// <summary>
/// RestWebHook
/// </summary>
public class RestWebHook : RestContentBase
{
    public RestWebHook()
    {
        
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// EventName
    /// </summary>
    public virtual string EventName { get; set; }

    /// <summary>
    /// TargetUrl
    /// </summary>
    public virtual string TargetUrl { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public virtual string Description { get; set; }
}
