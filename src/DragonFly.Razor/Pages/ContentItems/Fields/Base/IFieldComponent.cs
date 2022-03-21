using DragonFly.Content;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Pages.ContentItems.Fields;

public interface IFieldComponent : IComponent
{
    ContentField Field { get; }

    ContentFieldOptions Options { get; }
}
