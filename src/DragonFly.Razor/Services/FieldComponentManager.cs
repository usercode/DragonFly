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
    public class FieldComponentManager
    {
        private IDictionary<Type, Type> _cacheFieldView;
        private IDictionary<Type, Type> _cacheFieldOptionsView;

        public FieldComponentManager()
        {
            _cacheFieldView = new Dictionary<Type, Type>();
            _cacheFieldOptionsView = new Dictionary<Type, Type>();
        }

        public void RegisterField<TFieldComponent>()
            where TFieldComponent : IFieldComponent
        {
            Type fieldType = typeof(TFieldComponent).GetProperty(nameof(IFieldComponent.Field)).PropertyType;

            if(_cacheFieldView.TryAdd(fieldType, typeof(TFieldComponent)) == false)
            {
                _cacheFieldView[fieldType] = typeof(TFieldComponent);
            }
        }

        public void RegisterOptions<TFieldComponent>()
           where TFieldComponent : IFieldOptionsComponent
        {
            Type fieldOptionsType = typeof(TFieldComponent).GetProperty(nameof(IFieldOptionsComponent.Options)).PropertyType;

            if (_cacheFieldOptionsView.TryAdd(fieldOptionsType, typeof(TFieldComponent)))
            {
                _cacheFieldOptionsView[fieldOptionsType] = typeof(TFieldComponent);
            }
        }

        public RenderFragment CreateFieldComponent(ContentField contentField, ContentFieldOptions options)
        {
            if (_cacheFieldView.TryGetValue(contentField.GetType(), out Type viewType))
            {
                return builder =>
                {
                    builder.OpenComponent(0, viewType);
                    builder.AddAttribute(0, nameof(IFieldComponent.Field), contentField);
                    builder.AddAttribute(1, nameof(IFieldComponent.Options), options);
                    builder.CloseComponent();
                };
            }
            else
            {
                return builder => { builder.OpenElement(0, "p"); builder.AddContent(0, "no field available"); builder.CloseElement(); };
            }
        }

        public RenderFragment CreateOptionsComponent(ContentFieldOptions options)
        {
            if(options != null && _cacheFieldOptionsView.TryGetValue(options.GetType(), out Type viewType))
            {
                return builder => {
                    builder.OpenComponent(0, viewType);
                    builder.AddAttribute(0, nameof(IFieldOptionsComponent.Options), options);
                    builder.CloseComponent();
                };
            }
            else
            {
                return builder => { builder.OpenElement(0, "div"); builder.AddContent(0, "no options available"); builder.CloseElement(); };
            }            
        }
    }
}
