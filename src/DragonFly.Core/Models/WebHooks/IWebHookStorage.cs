// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly;

public interface IWebHookStorage
{
    Task<QueryResult<WebHook>> QueryAsync(WebHookQuery query);

    Task<WebHook> GetAsync(Guid id);

    Task CreateAsync(WebHook webHook);

    Task UpdateAsync(WebHook webHook);

    Task DeleteAsync(WebHook webHook);
}
