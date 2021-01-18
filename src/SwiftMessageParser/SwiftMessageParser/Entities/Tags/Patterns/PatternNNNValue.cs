using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternNNNValue : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            Qualifier = resultText.ParseWithStringAndIndex(TagName + ":", 3);
            Value = resultText.ToEndOfString(Qualifier).Trim();
            return this;
        }
    }
}
