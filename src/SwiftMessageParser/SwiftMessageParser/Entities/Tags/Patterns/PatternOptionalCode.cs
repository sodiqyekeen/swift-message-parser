using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternOptionalCode : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            TagName = resultText.ParseFromString(":", ":");
            if (resultText.Contains("//"))
            {
                Qualifier = resultText.Between("::", "/");
                Value = resultText.ParseWithStringAndIndex(Qualifier + "//", 4);
            }
            else
            {
                Qualifier = resultText.Between("::", "/");
                Code = resultText.ParseFromString(Qualifier + "/", "/");
                Value = resultText.ParseWithStringAndIndex(Code + "/", 4);
            }
            return this;
        }
    }
}
