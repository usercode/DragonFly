using DragonFly.Assets;
using DragonFly.Content;
using DragonFly.Razor.Pages.ContentItems.Fields;
using DragonFly.Razor.Pages.ContentSchemas.Fields;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Services
{
    public class AssetMetadataComponentManager
    {
        private IDictionary<Type, Type> _cacheView;

        public AssetMetadataComponentManager()
        {
            _cacheView = new Dictionary<Type, Type>();
        }

        public void Register<TMetadataComponent>()
            where TMetadataComponent : IAssetMetadataComponent
        {
            Type fieldType = typeof(TMetadataComponent).GetProperty(nameof(IAssetMetadataComponent.Metadata)).PropertyType;

            if(_cacheView.TryAdd(fieldType, typeof(TMetadataComponent)) == false)
            {
                _cacheView[fieldType] = typeof(TMetadataComponent);
            }
        }

        public RenderFragment CreateMetadataComponent(AssetMetadata metadata)
        {
            if (_cacheView.TryGetValue(metadata.GetType(), out Type viewType))
            {
                return builder =>
                {
                    builder.OpenComponent(0, viewType);
                    builder.AddAttribute(0, nameof(IAssetMetadataComponent.Metadata), metadata);
                    builder.CloseComponent();
                };
            }
            else
            {
                return builder => { builder.OpenElement(0, "p"); builder.AddContent(0, "no metadata available"); builder.CloseElement(); };
            }
        }
    }
}
