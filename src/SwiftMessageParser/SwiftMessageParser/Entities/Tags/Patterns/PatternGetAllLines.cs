using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternGetAllLines : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            Value = resultText.ToEndOfString(TagName + ":").TrimAllNewLines();
            return this;
        }
    }
}
