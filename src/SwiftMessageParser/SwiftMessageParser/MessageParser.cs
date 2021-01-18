using System;
using System.Collections.Generic;
using System.Linq;
using SwiftMessageParser.Entities;
using SwiftMessageParser.Entities.MT;
using SwiftMessageParser.Extensions;

namespace SwiftMessageParser
{
    public static class MessageParser 
    {
        /// <summary>
        /// Parses the swift message.
        /// </summary>
        /// <param name="swiftFormattedMessage">The swift formatted message.</param>
        public static List<SwiftMessage> Parse(string swiftFormattedMessage)
        {
            var messages = swiftFormattedMessage.Trim().Split(new string[] { "{5:" }, StringSplitOptions.RemoveEmptyEntries);
            List<SwiftMessage> swiftMessages = new List<SwiftMessage>();
            foreach (var message in messages)
            {
                if (message.Contains("{1:"))
                {
                    SwiftMessage swiftMessage = new SwiftMessage(message.LastIndexToEndOfString("{1:"));
                    swiftMessages.Add(swiftMessage);
                }
            }
            return swiftMessages;
        }

        /// <summary>
        /// Parses the swift formatted text to exact message type object <see cref="MessageTypes"/>.
        /// </summary>
        /// <param name="swiftFormattedFile">The swift formatted file.</param>
        /// <returns></returns>
        public static List<IMessageType> ParseExact(string swiftFormattedFile)
        {
            var swiftMessages = Parse(swiftFormattedFile);
            var validMessageTypes = Enum.GetValues(typeof(MessageTypes)).AsListOfString();

            _ = swiftMessages.RemoveAll(m => !validMessageTypes.Any(t => t.Contains(m.ApplicationHeader.MessageType)));
            List<IMessageType> allMessages = swiftMessages.Select(swiftMessage => swiftMessage.ToExact()).ToList();
            return allMessages;
        }
    }
}
