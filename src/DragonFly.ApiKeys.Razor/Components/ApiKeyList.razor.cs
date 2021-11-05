using DragonFly.Client.Base;
using DragonFLy.ApiKeys;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.ApiKeys.Razor.Components
{
    public class ApiKeyListBase : EntityListComponent<ApiKey>
    {
        public ApiKeyListBase()
        {
            Items = new List<ApiKey>();
        }

        [Inject]
        public IApiKeyService ApiKeyService { get; set; }

        public IEnumerable<ApiKey> Items { get; set; }    

        protected override string GetNavigationPath(ApiKey entity)
        {
            return $"/settings/apikey/{entity.Id}";
        }

        protected override async Task RefreshActionAsync()
        {
            Items = await ApiKeyService.GetAllApiKeys();
        }
    }
}
