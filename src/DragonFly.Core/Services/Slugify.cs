using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static class Slugify
    {
        public static string ToSlug(this string? phrase)
        {
            if (phrase == null)
            {
                return string.Empty;
            }

            string str = phrase.ToLower().Trim();

            str = Regex.Replace(str, "ä", "ae", RegexOptions.Compiled);
            str = Regex.Replace(str, "ö", "oe", RegexOptions.Compiled);
            str = Regex.Replace(str, "ü", "ue", RegexOptions.Compiled);
            str = Regex.Replace(str, @"\s+", "-", RegexOptions.Compiled);
            str = Regex.Replace(str, @"_+", "-", RegexOptions.Compiled);
            str = Regex.Replace(str, @"-+", "-", RegexOptions.Compiled);
            str = Regex.Replace(str, @"[^a-z0-9-.]", "", RegexOptions.Compiled);
            
            return str.Trim();
        }
    }
}
