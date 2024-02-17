// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore;

public class DragonFlyOptions
{
    public ContentVersionKind EnableVersioning { get; set; } = ContentVersionKind.PublishedOnly;
}
