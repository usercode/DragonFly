using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.REST.Models;
using DragonFly.Content;
using DragonFly.Contents.Assets;
using DragonFly.Contents.Content;
using DragonFly.Core;
using DragonFly.Data;
using DragonFly.Data.Models;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DragonFly.Client
{
    /// <summary>
    /// ContentService
    /// </summary>
    public partial class ClientContentService
    {
        public ClientContentService(HttpClient httpClient)
        {
            Client = httpClient;

            Client.DefaultRequestHeaders.Add("ApiKey", "1111");
        }

        /// <summary>
        /// Client
        /// </summary>
        public HttpClient Client { get; }

        
    }
}
