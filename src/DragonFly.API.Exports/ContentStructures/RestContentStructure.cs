// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Contents.Content;

namespace DragonFly.AspNetCore.API.Models;

public class RestContentStructure : RestContentBase
{
    public RestContentStructure()
    {
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }


}
