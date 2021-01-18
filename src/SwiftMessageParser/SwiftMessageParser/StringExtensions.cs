using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace SwiftMessageParser.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Gets the substring between the index of string a and b from the source string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static string Between(this string value, string a, string b)
        {
            int startIndex1 = value.IndexOf(a);
            int num = value.IndexOf(b, startIndex1);
            if (startIndex1 == -1 || num == -1) return string.Empty;
            int startIndex2 = startIndex1 + a.Length;
            return startIndex2 >= num ? string.Empty : value.Substring(startIndex2, num - startIndex2);
        }

        public static string Before(this string value, string str) =>
            value.IndexOf(str) > 1 ?
            value.Substring(0, value.IndexOf(str)) :
            value;

        public static string After(this string value, string str) =>
            value.IndexOf(str) > -1 ?
            value.Substring(value.IndexOf(str) + str.Length) :
            value;

        public static string RemoveSpecialCharactersAndLetter(this string value) =>
            new string(value.Where(c => char.IsDigit(c)).ToArray());


        /// <summary>
        /// Gets the amount value from field61 of MT942 message.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static string AmountFromField61(this string value, string a, string b)
        {
            int startIndex1 = value.IndexOf(a);
            int num = value.IndexOf(b, startIndex1);
            if (startIndex1 == -1 || num == -1)
                return "";
            int startIndex2 = startIndex1 + a.Length;
            if (startIndex2 >= num)
                return "";
            string temp = value.Substring(startIndex2, num - startIndex2 + 3).Replace(",", ".");
            return decimal.TryParse(temp, out _)
                ? temp
                : decimal.TryParse(temp.Substring(0, temp.Length - 1), out _)
                ? temp.Substring(0, temp.Length - 1)
                : temp.Substring(0, temp.Length - 3);
        }

        /// <summary>
        /// Parses from string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static string ParseFromString(this string value, string a, string b)
        {
            int num1 = value.IndexOf(a);
            string str = value.Substring(num1 + a.Length);
            int length = str.IndexOf(b);
            return num1 == -1 || length == -1 ? "" : str.Substring(0, length);
        }


        /// <summary>
        /// Parses the index of the with string and.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="a">a.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static string ParseWithStringAndIndex(this string value, string a, int index)
        {
            int num1 = value.IndexOf(a);
            int num2 = index;
            if (num1 == -1 || num2 == -1)
                return "";
            int startIndex = num1 + a.Length;
            return value.Substring(startIndex, index);
        }


        /// <summary>
        /// Gets the substring from the index of string a to end of source string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="a">a.</param>
        /// <returns></returns>
        public static string ToEndOfString(this string value, string a)
        {
            int startIndex = value.IndexOf(a) + a.Length;
            return value.Substring(startIndex);
        }

        /// <summary>
        /// Returns the substring from the Lasts index of the parameter to end of the source string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string LastIndexToEndOfString(this string value, string str)
        {
            int startIndex = value.LastIndexOf(str);
            return value.Substring(startIndex);
        }

        /// <summary>
        /// Trims all new lines.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string TrimAllNewLines(this string value) =>
             value.Replace(Environment.NewLine, " ").Trim();

        /// <summary>
        /// Trims the colon.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string TrimColon(this string value) => value.Trim(':');

        /// <summary>
        /// Trims the end of swift message.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string TrimEndOfSwift(this string value) => value.Trim('-', '}');

        /// <summary>
        /// Gets the swift tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="a">a.</param>
        /// <returns></returns>
        public static int GetNextSwiftTagIndex(this string value, int a, List<string> swiftTags)
        {
            if (swiftTags.Any(t => value.Substring(a + 1).Contains($":{t}:")))
            {
                int num = 6;
                if (value.Substring(a, 2) == ":" || value.Substring(a, 1) != ":")
                    ++num;
                int startIndex = a + num;

                return value.IndexOf(":", startIndex) - a;
            }
            return -1;
        }

        /// <summary>
        /// Coverts to date.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <returns></returns>
        public static DateTime CovertToDate(this string value, string dateFormat) =>
            DateTime.ParseExact(value, dateFormat, null);
    }
}
