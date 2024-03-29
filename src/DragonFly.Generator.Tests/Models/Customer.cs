﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly.Tests;

[ContentItem]
public partial class Customer
{
    [StringField]
    private string? _firstname;

    [StringField]
    private string? _lastname;

    [StringField]
    private string? _street;

    [SlugField]
    private SlugField _slug;

    [StringField]
    private string? _remark;
}
