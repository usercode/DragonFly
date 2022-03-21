using DragonFly.Content;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Pages.ContentSchemas.Fields;

public interface IFieldOptionsComponent : IComponent
{
    public ContentFieldOptions Options { get; }
}
