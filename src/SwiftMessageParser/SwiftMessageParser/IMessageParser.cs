using System.Collections.Generic;
using SwiftMessageParser.Entities;
using SwiftMessageParser.Entities.MT;

namespace SwiftMessageParser
{
    public interface IMessageParser
    {
        List<SwiftMessage> Parse(string swiftFormattedMessage);
        Entities.MT.IMessageParser ParseExact(string swiftFormattedMessage, MessageTypes type);
        Entities.MT.IMessageParser ParseExact(SwiftMessage swiftFormattedMessage);
        List<Entities.MT.IMessageParser> ParseExact(string swiftFormattedFile);
        MessageTypes GetMessageType(string swiftFormattedMessage);

    }
}
