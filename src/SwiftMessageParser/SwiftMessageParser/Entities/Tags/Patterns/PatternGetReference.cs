using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternGetReference : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            if (resultText.Contains("::"))
                Value = resultText.ToEndOfString(TagName + "::").TrimAllNewLines();
            else
                Value = resultText.ToEndOfString(TagName + ":").TrimAllNewLines();
            return this;
        }
    }
}
