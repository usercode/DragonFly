using ImageWizard.Core.ImageLoaders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNet.Options
{
    class HttpLoaderConfigureOptions : IConfigureOptions<HttpLoaderOptions>
    {
        public HttpLoaderConfigureOptions(IOptions<DragonFlyOptions> options)
        {
            Options = options.Value;
        }

        public DragonFlyOptions Options { get; }

        public void Configure(HttpLoaderOptions options)
        {
            options.SetHeader("ApiKey", Options.ApiKey);
        }
    }
}
