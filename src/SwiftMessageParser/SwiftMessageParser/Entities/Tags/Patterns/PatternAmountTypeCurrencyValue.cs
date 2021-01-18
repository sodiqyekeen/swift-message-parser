using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternAmountTypeCurrencyValue : Tag, ITag
    {

        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            if (resultText.Contains("::"))
            {
                Qualifier = resultText.Between(TagName + "::", "/");
                Type = resultText.ParseFromString(Qualifier + "//", "/");
                Code = resultText.ParseWithStringAndIndex(Type + "/", 3);
                Value = resultText.ToEndOfString(Code).TrimAllNewLines();
            }
            else
            {
                Qualifier = resultText.Between(TagName + ":", "/");
                Type = resultText.ParseFromString(Qualifier + "//", "/");
                Code = resultText.ParseWithStringAndIndex(Type + "/", 3);
                Value = resultText.ToEndOfString(Code).TrimAllNewLines();
            }
            return this;
        }
    }
}
