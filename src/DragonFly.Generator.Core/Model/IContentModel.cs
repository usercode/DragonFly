// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IContentModel
{
    Guid Id { get; }
    ContentItem GetContentItem();
    static abstract IContentMetadata Metadata { get; }
}

