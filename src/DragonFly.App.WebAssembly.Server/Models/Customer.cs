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
    private string? _title;

    [StringField]
    private string? _lastname;

    [StringField]
    private string? _firstname;

}
