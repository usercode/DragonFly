// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IContentMetadata
{
    string ModelName { get; }

    IContentModel CreateModel(ContentItem contentItem);

    ContentSchema CreateSchema();
}
