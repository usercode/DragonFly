// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IContentInterceptor
{
    Task OnCreatedAsync(ContentItem contentItem);

    Task OnUpdatedAsync(ContentItem contentItem);

    Task OnDeletedAsync(ContentItem contentItem);

    Task OnPublishedAsync(ContentItem contentItem);

    Task OnUnpublishedAsync(ContentItem contentItem);

    Task OnPublishedAsync(Asset asset);
}
