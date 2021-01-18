using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
  public class PatternDoubleSlashTypeQuantity : Tag, ITag
  {
    public ITag GetTagValues(string resultText)
    {
      GetTagName(resultText);
      if (resultText.Contains("::"))
      {
        Qualifier = resultText.Between(TagName + "::", "/");
        Type = resultText.ParseFromString(Qualifier + "//", "/");
        Value = resultText.ToEndOfString(Type + "/").TrimAllNewLines();
      }
      else
      {
        Qualifier = resultText.Between(TagName + ":", "/");
        Type = resultText.ParseFromString(Qualifier + "//", "/");
        Value = resultText.ToEndOfString(Type + "/").TrimAllNewLines();
      }
      return this;
    }
  }
}
