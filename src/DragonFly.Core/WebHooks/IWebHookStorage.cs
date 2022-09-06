using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public interface IWebHookStorage
{
    Task<QueryResult<WebHook>> QueryAsync(WebHookQuery query);

    Task<WebHook> GetAsync(Guid id);

    Task CreateAsync(WebHook webHook);

    Task UpdateAsync(WebHook webHook);

    Task DeleteAsync(WebHook webHook);
}
