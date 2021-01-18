using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternOptionalSingleSlashSeperator : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            if (resultText.Contains("/"))
            {
                Qualifier = resultText.ParseFromString(TagName + ":", "/");
                Value = resultText.ToEndOfString(Qualifier + "/");
            }
            else
                Value = resultText.ToEndOfString(TagName + ":");
            return this;
        }
    }
}
