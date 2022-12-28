// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API;

public class RestContentNode : RestContentBase
{
    public RestContentNode()
    {
    }

    public Guid? Structure { get; set; }

    public RestContentNode Parent { get; set; }
}
