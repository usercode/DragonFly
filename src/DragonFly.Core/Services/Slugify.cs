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

            str = str.Replace("ä", "ae");
            str = str.Replace("ö", "oe");
            str = str.Replace("ü", "ue");
            str = str.Replace("ß", "ss");

            str = Regex.Replace(str, @"\s+", "-", RegexOptions.Compiled);
            str = Regex.Replace(str, @"_+", "-", RegexOptions.Compiled);           
            str = Regex.Replace(str, @"[^a-z0-9-.]", "-", RegexOptions.Compiled);
            str = Regex.Replace(str, @"-+", "-", RegexOptions.Compiled);

            return str.Trim('-');
        }
    }
}
