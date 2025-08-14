// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Mediator;
using SmartResults;

namespace DragonFly;

public record class UpdateContentItem : ICommand<Result>
{
    public ContentItem? Content { get; set; }
}
