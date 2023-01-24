﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IContentInterceptor
{
    Task OnCreatedAsync(IDataStorage storage, ContentItem contentItem);

    Task OnUpdatedAsync(IDataStorage storage, ContentItem contentItem);

    Task OnDeletedAsync(IDataStorage storage, ContentItem contentItem);

    Task OnPublishingAsync(IDataStorage storage, ContentItem contentItem);

    Task OnPublishedAsync(IDataStorage storage, ContentItem contentItem);

    Task OnUnpublishedAsync(IDataStorage storage, ContentItem contentItem);

    Task OnPublishedAsync(IDataStorage storage, Asset asset);
}
