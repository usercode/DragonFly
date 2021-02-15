using DragonFly.Content.ContentParts;
using DragonFly.Data.Content.ContentParts;
using DragonFly.Razor.Pages.ContentItems.Fields;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Services
{
    public class FieldComponentManager
    {
        private IDictionary<Type, Type> _cache;

        public FieldComponentManager()
        {
            _cache = new Dictionary<Type, Type>();
        }

        public void Register<TFieldComponent>()
            where TFieldComponent : IFieldComponent
        {
            Type fieldType = typeof(TFieldComponent).GetProperty(nameof(IFieldComponent.Field)).PropertyType;

            if(_cache.TryAdd(fieldType, typeof(TFieldComponent)) == false)
            {
                _cache[fieldType] = typeof(TFieldComponent);
            }

        }

        public RenderFragment CreateFieldComponent(ContentField contentField, ContentFieldOptions options)
        {
            Type viewType = _cache[contentField.GetType()];

            return builder => { 
                builder.OpenComponent(0, viewType); 
                builder.AddAttribute(0, nameof(IFieldComponent.Field), contentField);
                builder.AddAttribute(1, nameof(IFieldComponent.Options), options);
                builder.CloseComponent();
            };
        }
    }
}
