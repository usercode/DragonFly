// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public class IntegerFieldOptions : SingleValueFieldOptions<long>
{
    public long? MinValue { get; set; }
    public long? MaxValue { get; set; }

    public override IContentField CreateContentField()
    {
        return new IntegerField(DefaultValue);
    }
}
