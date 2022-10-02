// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public class BoolFieldOptions : SingleValueFieldOptions<bool>
{
    public override IContentField CreateContentField()
    {
        return new BoolField(DefaultValue);
    }
}
