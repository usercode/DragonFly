using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Razor.Base
{
    public class ElementComponent<TElement> : ComponentBase, IElementComponent
        where TElement : Element
    {
        [Parameter]
        public TElement Element { get; set; }

        Element IElementComponent.Element => Element;
    }
}
