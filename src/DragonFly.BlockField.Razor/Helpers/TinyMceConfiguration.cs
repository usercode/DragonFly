using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField.Razor.Helpers;

public class TinyMceConfiguration
{
    public static Dictionary<string, object> Editor = new Dictionary<string, object>
    {
        { "menubar", true },
        { "plugins", "link image code table lists autoresize" },
        { "toolbar", "undo redo | forecolor | numlist bullist | bold italic | alignleft aligncenter alignright alignjustify | link image" }
    };
}
