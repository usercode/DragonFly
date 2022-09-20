using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField;

/// <summary>
/// Block
/// </summary>
public abstract class Block
{
    public virtual string Type => GetType().Name;

    private string? _name;

    public string Name
    {
        get
        {
            if (_name == null)
            {
                _name = CreateName();
            }

            return _name;
        }
    }

    protected virtual string CreateName()
    {
        string name = GetType().Name;

        string blockSuffix = "Block";

        if (name.EndsWith(blockSuffix))
        {
            name = name[0..^blockSuffix.Length];
        }

        return name;
    }

    public virtual string CssIcon => "bi bi-app";

    public override string ToString()
    {
        return Name;
    }
}
