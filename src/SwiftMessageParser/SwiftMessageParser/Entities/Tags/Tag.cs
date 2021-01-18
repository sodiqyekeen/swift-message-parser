using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.Tags
{
  public class Tag
  {
    public string TagName { get; set; }

    public string Qualifier { get; set; }

    public string Type { get; set; }

    public string Code { get; set; }

    public string Value { get; set; }

    public string Description { get; set; }

    public void GetTagName(string swiftText)
    {
      TagName = swiftText.ParseFromString(":", ":");
    }
  }
}
