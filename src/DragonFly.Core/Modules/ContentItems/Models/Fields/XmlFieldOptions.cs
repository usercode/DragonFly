// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class XmlFieldOptions : SingleValueFieldOptions<string?>
{
    /// <summary>
    /// MinLength
    /// </summary>
    public int MinLength { get; set; }

    /// <summary>
    /// MaxLength
    /// </summary>
    public int MaxLength { get; set; }

    public override ContentField CreateContentField()
    {
        return new XmlField(DefaultValue);
    }
}
