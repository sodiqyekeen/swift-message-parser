using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternCodeWithNarrative : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            GetTagName(resultText);
            Qualifier = resultText.Between("::", "/");
            Code = resultText.ParseFromString(Qualifier + "/", "/");
            Value = resultText.ToEndOfString(Code + "/");
            return this;
        }
    }
}
