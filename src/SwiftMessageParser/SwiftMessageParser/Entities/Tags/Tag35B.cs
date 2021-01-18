using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags
{
    public class Tag35B : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            if (resultText.Substring(5, 4) == "ISIN")
            {
                Qualifier = resultText.Substring(5, 4);
                Value = resultText.ParseWithStringAndIndex("ISIN ", 12);
                Description = resultText.ToEndOfString(Value).Trim();
                TagName = "35B";
            }
            else
            {
                Qualifier = resultText.ParseWithStringAndIndex(":/", 2);
                Value = resultText.ToEndOfString(Qualifier + "/");
            }
            return this;
        }

        public Tag35B()
        {
            TagName = "35B";
        }
    }
}
