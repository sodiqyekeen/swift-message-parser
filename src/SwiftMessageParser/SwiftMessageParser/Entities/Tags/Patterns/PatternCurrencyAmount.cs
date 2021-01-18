using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
  public class PatternCurrencyAmount : Tag, ITag
  {
    public ITag GetTagValues(string resultText)
    {
      GetTagName(resultText);
      Code = resultText.ParseWithStringAndIndex(TagName + ":", 3);
      Value = resultText.ToEndOfString(Code).Trim();
      return this;
    }
  }
}
