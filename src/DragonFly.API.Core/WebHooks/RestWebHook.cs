// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API;

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

    /// <summary>
    /// Headers
    /// </summary>
    public virtual IList<HeaderItem> Headers { get; set; } = new List<HeaderItem>();
}
