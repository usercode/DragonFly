// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Mediator;
using SmartResults;

namespace DragonFly;

public record class CreateContentItem : ICommand<Result>
{
    public ContentItem? Content { get; set; }
}
