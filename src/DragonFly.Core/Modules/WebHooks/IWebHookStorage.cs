// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using SmartResults;

namespace DragonFly;

public interface IWebHookStorage
{
    Task<Result<QueryResult<WebHook>>> QueryAsync(WebHookQuery query);

    Task<Result<WebHook?>> GetAsync(Guid id);

    Task<Result> CreateAsync(WebHook webHook);

    Task<Result> UpdateAsync(WebHook webHook);

    Task<Result> DeleteAsync(WebHook webHook);
}
