using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternSignCurrencyAmount : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            if (resultText.Contains("+") || resultText.Contains("-"))
            {
                Qualifier = resultText.Between("::", "/");
                string withStringAndIndex = resultText.ParseWithStringAndIndex("//", 1);
                Code = resultText.ParseWithStringAndIndex(withStringAndIndex, 3);
                Value = withStringAndIndex + resultText.ToEndOfString(Code);
            }
            else
            {
                Qualifier = resultText.Between("::", "/");
                Code = resultText.ParseWithStringAndIndex("//", 3);
                Value = resultText.ToEndOfString(Code);
            }
            return this;
        }
    }
}
