using SwiftMessageParser.Extensions;
using System;

namespace SwiftMessageParser.Entities.Tags.Pattern
{
    public class PatternSlashConditionalLines : Tag, ITag
    {
        public ITag GetTagValues(string resultText)
        {
            if (resultText.Contains(":59:"))
                resultText = resultText.Replace("//", string.Empty);

            GetTagName(resultText);
            if (resultText.Contains("/D/"))
            {
                Code = "D";
                if (resultText.Contains(Environment.NewLine))
                {
                    if (resultText.IndexOf("\n") != resultText.Length)
                    {
                        Value = resultText.ToEndOfString("/D/").TrimAllNewLines();
                        return this;
                    }
                    Value = resultText.ParseFromString("/D", Environment.NewLine).TrimAllNewLines();
                    Description = resultText.ToEndOfString(Value).TrimAllNewLines();
                    return this;
                }
                Value = resultText.ToEndOfString("/D/");
                return this;
            }
            if (resultText.Contains("/C/"))
            {
                Code = "C";
                if (resultText.Contains(Environment.NewLine))
                {
                    if (resultText.IndexOf("\n") != resultText.Length)
                    {
                        Value = resultText.ToEndOfString("/C/").TrimAllNewLines();
                        return this;
                    }
                    Value = resultText.ParseFromString("/C/", Environment.NewLine).TrimAllNewLines();
                    Description = resultText.ToEndOfString(Value).TrimAllNewLines();
                    return this;
                }
                Value = resultText.ToEndOfString("/C/");
                return this;
            }
            if (resultText.Contains("//"))
            {
                if (resultText.IndexOf("\n") != resultText.Length)
                {
                    Value = resultText.ToEndOfString("//");
                    return this;
                }
                Value = resultText.ParseFromString("//", Environment.NewLine);
                Description = resultText.ToEndOfString(Value).TrimAllNewLines();
                return this;
            }
            if (resultText.Contains("\n"))
            {
                if (resultText.IndexOf("\n") != resultText.Length)
                {
                    if (resultText.Contains("/"))
                    {
                        Value = resultText.Between("/", "\n").Trim();
                        string tempString = resultText.ToEndOfString(Value).Trim();
                        if (tempString.Contains("\n"))
                        {
                            Qualifier = tempString.Substring(0, tempString.IndexOf("\n") + 1).Trim();
                            Description = tempString.ToEndOfString(Qualifier).TrimAllNewLines();
                        }
                        else
                        {
                            Qualifier = resultText.ToEndOfString(Value).TrimAllNewLines();
                        }

                        return this;
                    }
                    Value = resultText.Between(TagName + ":", Environment.NewLine);
                    string tempString2 = resultText.ToEndOfString(Value).Trim();
                    if (tempString2.Contains("\n"))
                    {
                        Qualifier = tempString2.Substring(0, tempString2.IndexOf("\n") + 1).Trim();
                        Description = tempString2.ToEndOfString(Qualifier).TrimAllNewLines();
                    }
                    else
                    {
                        Qualifier = resultText.ToEndOfString(Value).TrimAllNewLines();
                    }
                    return this;
                }
                if (resultText.Contains("/"))
                {
                    Value = resultText.Between("/", Environment.NewLine);
                    return this;
                }
                Value = resultText.Between(TagName + ":", Environment.NewLine);
                return this;
            }
            if (resultText.Contains("/"))
            {
                Value = resultText.ToEndOfString("/");
                return this;
            }
            Value = resultText.ToEndOfString(TagName + ":").Trim();
            return this;
        }
    }
}
