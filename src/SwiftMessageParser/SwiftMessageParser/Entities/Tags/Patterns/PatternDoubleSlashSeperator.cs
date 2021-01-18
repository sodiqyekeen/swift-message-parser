using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
  public class PatternDoubleSlashSeperator : Tag, ITag
  {
    public ITag GetTagValues(string resultText)
    {
      GetTagName(resultText);
      if (resultText.Contains("::"))
      {
        Qualifier = resultText.Between(TagName + "::", "/").Trim();
        Value = resultText.ToEndOfString("//").TrimAllNewLines();
      }
      else
      {
        Qualifier = resultText.Between(TagName + ":", "/").Trim();
        Value = resultText.ToEndOfString("//").TrimAllNewLines();
      }
      return this;
    }
  }
}
