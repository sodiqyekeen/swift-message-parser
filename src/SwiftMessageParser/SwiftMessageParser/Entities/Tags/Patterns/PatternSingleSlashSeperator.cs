using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternSingleSlashSeperator : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            Qualifier = resultText.ParseFromString(TagName + ":", "/");
            Value = resultText.ToEndOfString(Qualifier + "/");
            return this;
        }
    }
}
