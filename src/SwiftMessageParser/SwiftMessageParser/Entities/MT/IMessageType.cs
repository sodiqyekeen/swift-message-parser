namespace SwiftMessageParser.Entities.MT
{
    public interface IMessageType
    {
        void Parse(SwiftMessage swiftMessage);
    }
}
