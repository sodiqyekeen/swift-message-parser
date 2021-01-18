using System;
using System.Collections.Generic;
using SwiftMessageParser.Entities;
using SwiftMessageParser.Entities.MT;

namespace SwiftMessageParser
{
    internal static class Util
    {
        public static IMessageType GetParser(this MessageTypes messageType) =>
           messageType switch
           {
               MessageTypes.MT103 => new MT103(),
               MessageTypes.MT910 => new MT910(),
               MessageTypes.MT942 => new MT942(),
               _ => throw new ArgumentException(Exceptions.InvalidType)
           };

        public static List<string> AsListOfString(this Array array)
        {
            List<string> list = new List<string>();
            foreach (var item in array)
            {
                list.Add(item.ToString());
            }
            return list;
        }
    }
}
