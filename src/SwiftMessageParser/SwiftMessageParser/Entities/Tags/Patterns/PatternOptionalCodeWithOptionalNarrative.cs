using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternOptionalCodeWithOptionalNarrative : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            if (resultText.Contains("//"))
            {
                Qualifier = resultText.Between("::", "/");
                Value = resultText.ParseWithStringAndIndex(Qualifier + "//", 4);
                Description = resultText.ToEndOfString(Value);
            }
            else
            {
                Qualifier = resultText.Between("::", "/");
                Code = resultText.ParseFromString(Qualifier + "/", "/");
                Value = resultText.ToEndOfString(Code + "/");
                Description = resultText.ToEndOfString(Value);
            }
            return this;
        }
    }
}
