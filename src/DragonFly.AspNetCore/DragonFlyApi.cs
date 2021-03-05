using DragonFly.Content;
using DragonFly.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore
{
    public class DragonFlyApi : IDragonFlyApi
    {
        public DragonFlyApi(
            ContentFieldManager fieldManager, 
            AssetMetadataManager assetMetadata,
            IDataStorage dataStorage)
        {
            Fields = fieldManager;
            AssetMetadatas = assetMetadata;
            DataStorage = dataStorage;
        }

        /// <summary>
        /// Fields
        /// </summary>
        public ContentFieldManager Fields { get; }

        /// <summary>
        /// AssetMetadatas
        /// </summary>
        public AssetMetadataManager AssetMetadatas { get; }

        /// <summary>
        /// DataStorage
        /// </summary>
        public IDataStorage DataStorage { get; }

        public void Init()
        {
            
        }


    }
}
