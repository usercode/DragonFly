// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Query;

public class FieldOrder
{
    public FieldOrder()
    {
        Name = string.Empty;
        Asc = true;
    }

    public FieldOrder(string name, bool asc)
    {
        Name = name;
        Asc = asc;
    }

    public string Name { get; set; }

    public bool Asc { get; set; }
}
