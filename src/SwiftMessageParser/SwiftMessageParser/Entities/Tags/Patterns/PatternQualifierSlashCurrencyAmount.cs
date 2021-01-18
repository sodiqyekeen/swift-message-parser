using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternQualifierSlashCurrencyAmount : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            if (resultText.Contains("//"))
            {
                Qualifier = resultText.ParseFromString(TagName + ":", "/");
                Code = resultText.ParseWithStringAndIndex(Qualifier + "//", 3);
                Value = resultText.ToEndOfString(Code);
            }
            else
            {
                Qualifier = resultText.ParseFromString(TagName + ":", "/");
                Code = resultText.ParseWithStringAndIndex(Qualifier + "/", 3);
                Value = resultText.ToEndOfString(Code);
            }
            return this;
        }
    }
}
