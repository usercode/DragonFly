// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
