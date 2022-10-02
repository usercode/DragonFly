// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;
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

namespace DragonFly.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService
{
    public ClientContentService(HttpClient httpClient)
    {
        Client = httpClient;
    }

    /// <summary>
    /// Client
    /// </summary>
    public HttpClient Client { get; }

    
}
