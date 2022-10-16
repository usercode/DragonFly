// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Contents.Content;

namespace DragonFly.AspNetCore.API.Models.Assets;

public class RestAssetFolder : RestContentBase
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; set; }
}
