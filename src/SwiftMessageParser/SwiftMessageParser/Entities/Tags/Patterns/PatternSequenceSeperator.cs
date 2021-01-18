namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternSequenceSeperator : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            return this;
        }
    }
}
