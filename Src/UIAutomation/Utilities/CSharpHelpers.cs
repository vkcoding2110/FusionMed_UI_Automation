using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace UIAutomation.Utilities
{
    public class CSharpHelpers
    {
        public int GenerateRandomNumber()
        {
            var random = new Random();
            return random.Next(1000000000,2147483647);
        }

        public decimal ConvertCurrencyToNumber(string currency)
        {
            return decimal.Parse(currency.Replace(".00", "").Replace("$",""), NumberStyles.Currency);
        }

        public IList<string> GetJsonObjectChildrenToStringList(JObject jObject,string arrayName)
        {
            IList<JToken> jSonList = jObject[arrayName].Children().ToList();
            return jSonList.Select(result => result.ToObject<string>()).ToList();
        }
        public IList<string> StringToList(string text, char splitBy)
        {
            return text.Split(splitBy).Select(s => s.Trim()).ToList();
        }
    }
    public static class StringExtensions
    {
        private static readonly Regex WhiteSpaceRegex = new Regex(@"\s+");
        public static string RemoveWhitespace(this string stringWithSpaces) => WhiteSpaceRegex.Replace(stringWithSpaces, "");
        public static string RemoveEndOfTheLineCharacter(this string data)
        {
            return data.Replace("\r", "").Replace("\n", "");
        }
        public static string HtmlToString(this string htmlText)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return reg.Replace(htmlText, "");
        }
    }


    public static class IntExtensions
    {
        public static bool IsWithin(this int value, int minimum, int maximum)
        {
            return value >= minimum && value <= maximum;
        }
    }

}
