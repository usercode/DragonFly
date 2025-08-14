// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using Mediator;
using SmartResults;

namespace DragonFly.AspNetCore.Handlers;

public class ContentQueryHandler : IQueryHandler<ContentQuery, Result<QueryResult<ContentItem>>>
{
    private readonly IContentStorage _contentStorage;
    public ContentQueryHandler(IContentStorage contentStorage)
    {
        _contentStorage = contentStorage;
    }

    public async ValueTask<Result<QueryResult<ContentItem>>> Handle(ContentQuery query, CancellationToken cancellationToken)
    {
        return (await _contentStorage.QueryAsync(query))
                                        .ToResult(x =>
                                            {
                                                foreach (ContentItem contentItem in x.Items)
                                                {
                                                    contentItem.ApplySchema();
                                                    contentItem.Validate();
                                                }

                                                return x;
                                            });
    }
}
