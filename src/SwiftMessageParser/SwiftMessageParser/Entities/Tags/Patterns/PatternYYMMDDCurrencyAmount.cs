using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternYYMMDDCurrencyAmount : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            Qualifier = resultText.Substring(5, 6);
            Code = resultText.Substring(11, 3);
            Value = resultText.ToEndOfString(Code).Trim();
            return this;
        }
    }
}
