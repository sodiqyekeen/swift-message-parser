using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SwiftMessageParser.Demo
{
    static class BasicUtility
    {
        /// <summary>
        /// Gets the swift files.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>Returns a list of files picked from the specified directory.</returns>
        public static IEnumerable<string> GetSwiftFiles(string filePath)
        {
            try
            {
                if (Directory.Exists(filePath))
                {
                    return Directory.GetFiles(filePath);
                }
                else
                {
                    Console.WriteLine($"Directory: {filePath} does not exist!");
                    return Enumerable.Empty<string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Enumerable.Empty<string>();
            }
        }

        /// <summary>
        /// Coverts to date.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <returns></returns>
        public static DateTime CovertToDate(this string value, string dateFormat)
        {
            return DateTime.ParseExact(value, dateFormat, null);
        }


        /// <summary>
        /// Writes the file to the specified directory asynchronously.
        /// </summary>
        /// <param name="fileContent">Content of the file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="filePath">The file path.</param>
        public static async void WriteFileAsync(this string fileContent, string fileName, string filePath)
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string _path = $"{filePath}/{fileName}.txt";
                using (StreamWriter w = File.AppendText(_path))
                {
                    await w.WriteLineAsync(fileContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Moves file to directory.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="filePath">The file path.</param>
        public static void MoveToDirectory(this string file, string filePath)
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                if (File.Exists(filePath + Path.GetFileName(file)))
                    File.Delete(filePath + Path.GetFileName(file));

                File.Move(file, filePath + Path.GetFileName(file));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// Gets the currency symbol.
        /// </summary>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns>Returns currency symbol (string)</returns>
        public static string GetCurrencySymbol(this string currencyCode)
        {
            switch (currencyCode)
            {
                case "2":
                    return "USD";
                case "3":
                    return "GBP";
                case "46":
                    return "EUR";
                default:
                    return string.Empty;
            }
        }



        public static IEnumerable<T> GetEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static string RemoveExtension(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            int txt = fileName.IndexOf(".txt");

            return fileName.Substring(0, txt);
        }


    }
}
