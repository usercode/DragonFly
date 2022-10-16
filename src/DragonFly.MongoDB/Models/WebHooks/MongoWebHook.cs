// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

public class MongoWebHook : MongoContentBase
{
    public virtual string? Name { get; set; }

    public virtual string? EventName { get; set; }

    public virtual string? TargetUrl { get; set; }

    public virtual string? Description { get; set; }
}
