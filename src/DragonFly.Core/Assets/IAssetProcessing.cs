using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// IAssetProcessing
/// </summary>
public interface IAssetProcessing
{
    /// <summary>
    /// SupportedMimetypes
    /// </summary>
    /// <param name="asset"></param>
    /// <returns></returns>
    IEnumerable<string> SupportedMimetypes { get; }

    /// <summary>
    /// OnAssetChangedAsync
    /// </summary>
    /// <param name="asset"></param>
    /// <param name="openStream"></param>
    /// <returns></returns>
    Task OnAssetChangedAsync(IAssetProcessingContext context);

}
