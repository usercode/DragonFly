// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FieldOrder
/// </summary>
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

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Asc
    /// </summary>
    public bool Asc { get; set; }
}
