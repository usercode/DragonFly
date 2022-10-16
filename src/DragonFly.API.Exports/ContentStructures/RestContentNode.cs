// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Contents.Content;

namespace DragonFly.AspNetCore.API.Models;

public class RestContentNode : RestContentBase
{
    public RestContentNode()
    {
    }

    public Guid? Structure { get; set; }

    public RestContentNode Parent { get; set; }
}
