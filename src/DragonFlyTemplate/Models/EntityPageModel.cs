﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Proxy;

namespace DragonFlyTemplate.Models;

public class EntityPageModel : BasePageModel, IContentModel
{
    public virtual Guid Id { get; }

}
