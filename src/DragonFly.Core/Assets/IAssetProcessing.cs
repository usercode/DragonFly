using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    /// <summary>
    /// IAssetProcessing
    /// </summary>
    public interface IAssetProcessing
    {
        /// <summary>
        /// CanUse
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        bool CanUse(Asset asset);

        /// <summary>
        /// OnAssetChangedAsync
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="openStream"></param>
        /// <returns></returns>
        Task OnAssetChangedAsync(IAssetProcessingContext context);

    }
}
