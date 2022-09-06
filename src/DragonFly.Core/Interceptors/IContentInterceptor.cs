using DragonFly.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public interface IContentInterceptor
{
    Task OnUpdatedAsync(IDataStorage storage, ContentItem contentItem);

    Task OnDeletedAsync(IDataStorage storage, ContentItem contentItem);

    Task OnPublishingAsync(IDataStorage storage, ContentItem contentItem);

    Task OnPublishedAsync(IDataStorage storage, ContentItem contentItem);

    Task OnUnpublishedAsync(IDataStorage storage, ContentItem contentItem);

    Task OnPublishedAsync(IDataStorage storage, Asset asset);
}
