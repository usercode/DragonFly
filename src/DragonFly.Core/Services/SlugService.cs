// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.RegularExpressions;

namespace DragonFly;

/// <summary>
/// SlugService
/// </summary>
public partial class SlugService : ISlugService
{
    public string Transform(string? phrase)
    {
        if (phrase == null)
        {
            return string.Empty;
        }

        string str = phrase.ToLower().Trim();

        str = str.Replace("ä", "ae");
        str = str.Replace("ö", "oe");
        str = str.Replace("ü", "ue");
        str = str.Replace("ß", "ss");
    
        str = RegexRemoveSpecialCharacters().Replace(str, "-");
        str = RegexRemoveDuplicateMinus().Replace(str, "-");

        return str.Trim('-');
    }

    [GeneratedRegex("[^a-z0-9-]", RegexOptions.Compiled)]
    private static partial Regex RegexRemoveSpecialCharacters();

    [GeneratedRegex("-+", RegexOptions.Compiled)]
    private static partial Regex RegexRemoveDuplicateMinus();
}
