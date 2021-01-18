using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternOptionalSubFunction : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            if (resultText.Contains("/"))
            {
                Value = resultText.Between(TagName + ":", "/");
                Code = resultText.ParseWithStringAndIndex(Value + "/", 4).Trim();
            }
            else
                Value = resultText.ToEndOfString(TagName + ":").TrimColon().Trim();
            return this;
        }
    }
}
