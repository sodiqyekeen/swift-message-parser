using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Timers;
using Newtonsoft.Json;

namespace SwiftMessageParser.Demo
{
    class Program
    {
        const string baseFilePath = "C:\\FxInflow";
        static void Main(string[] args)
        {
            var timer = Stopwatch.StartNew();
            var files = BasicUtility.GetSwiftFiles(baseFilePath);

            foreach (var file in files)
            {
                try
                {
                   // var swiftMessages = MessageParser.Parse(File.ReadAllText(file));
                    ////Console.WriteLine(JsonConvert.SerializeObject(swiftMessages));
                  // MoveFile(file, swiftMessages.First().ApplicationHeader.MessageType);
                    var exactMessages = MessageParser.ParseExact(File.ReadAllText(file));
                    //Console.WriteLine(JsonConvert.SerializeObject(exactMessages));
                    MoveFile(file, exactMessages.First().GetType().Name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MoveFile(file, "ERROR");
                }
            }
            timer.Stop();
            Console.WriteLine("\n\n\n" + timer.ElapsedMilliseconds);
            _ = Console.ReadLine();
        }

        private static void MoveFile(string file, string folder)
        {
            string filePath = Path.Combine(baseFilePath, folder);
            if (!Directory.Exists(filePath))
            {
                _ = Directory.CreateDirectory(filePath);
            }
            if (File.Exists(filePath + Path.GetFileName(file)))
                File.Delete(filePath + Path.GetFileName(file));

            File.Move(file, Path.Combine(filePath, Path.GetFileName(file)));
        }
    }
}
