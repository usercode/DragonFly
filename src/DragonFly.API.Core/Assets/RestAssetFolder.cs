// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API;

public class RestAssetFolder : RestContentBase
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual Guid? Parent { get; set; }
}
