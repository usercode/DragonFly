using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore
{
    public interface IDragonFlyApi
    {
        void Init();

        /// <summary>
        /// Fields
        /// </summary>
        ContentFieldManager Fields { get; }

        /// <summary>
        /// AssetMetadatas
        /// </summary>
        AssetMetadataManager AssetMetadatas { get; }

        /// <summary>
        /// DataStorage
        /// </summary>
        IDataStorage DataStorage { get; }
    }
}
