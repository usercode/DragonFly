// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Identity;

public class ClaimItem
{
    public ClaimItem()
    {
            
    }

    public ClaimItem(string type, string value)
    {
        Type = type;
        Value = value;
    }

    public string Type { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;
}
