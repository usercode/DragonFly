// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Mediator;
using SmartResults;

namespace DragonFly;

public record class UnpublishContentItem : ICommand<Result>
{
    /// <summary>
    /// Schema
    /// </summary>
    public string Schema { get; set; } = string.Empty;

    /// <summary>
    /// ContentId
    /// </summary>
    public Guid ContentId { get; set; }
}
