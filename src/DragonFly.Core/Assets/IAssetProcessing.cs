﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public delegate Task<Stream> OpenAssetStream();

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
        Task OnAssetChangedAsync(Asset asset, OpenAssetStream openStream);

    }
}
