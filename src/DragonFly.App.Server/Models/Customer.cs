// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.Generator;

namespace DragonFlyABC;

[ContentItem]
public partial class Customer
{
    [StringField(Required = true, MaxLength = 1024)]
    public partial string? Title { get; set; }  

    [StringField]
    public partial string? Lastname { get; set; }

    [StringField]
    public partial string? Firstname { get; set; }

}
