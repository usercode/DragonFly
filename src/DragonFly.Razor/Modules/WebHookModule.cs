using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Modules
{
    /// <summary>
    /// WebHookModule
    /// </summary>
    public class WebHookModule : ClientModule
    {
        public override string Name => "Webhook";

        public override string Description => "Manage webhooks";

        public override string Author => "DragonFly";

        public override void Init(IDragonFlyApi api)
        {
            api.AddMenuItem("Webhook", "oi oi-list-rich icon", "webhook");
        }
    }
}
