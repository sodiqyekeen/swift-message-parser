using System.Collections.Generic;
using System.Linq;
using SwiftMessageParser.Extensions;

namespace SwiftMessageParser
{
    internal class MTParser
    {
        /// <summary>
        /// The swift tags
        /// </summary>
        private List<string> _swiftTags = new List<string>();

        /// <summary>
        /// The is tag
        /// </summary>
        private bool _isTag;

        /// <summary>
        /// Initializes a new instance of the <see cref="MTParser"/> class.
        /// </summary>
        public MTParser() => LoadSwiftTags();

        /// <summary>
        /// Block4s to list.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public List<string> Block4ToList(string message)
        {
            List<string> stringList = new List<string>();

            _isTag = false;
            int num = 0;
            int length1 = message.Length;
            while (num < length1)
            {
                int swiftTag = message.GetNextSwiftTagIndex(num, _swiftTags);
                if (swiftTag > 0)
                {
                    int length2 = CheckTag(num + swiftTag, length1, message);
                    if (_isTag)
                    {
                        string str = message.Substring(num, swiftTag);
                        stringList.Add(str.Trim());
                        num += swiftTag;
                        _isTag = false;
                    }
                    else
                    {
                        if (message.LastIndexOf(":") > num && length2 != length1)
                        {
                            swiftTag = length2 - num;
                            string str = message.Substring(num, swiftTag);
                            stringList.Add(str.Trim());
                            num += swiftTag;
                        }
                        else
                        {
                            string str = message.Substring(num, length2);
                            stringList.Add(str.TrimAllNewLines());
                            num = length2;
                            _isTag = false;
                        }
                    }
                }
                else
                {
                    string str = message.Substring(num, length1 - num);
                    stringList.Add(str.TrimEndOfSwift().Trim());
                    num = length1 + 1;
                }
            }
            return stringList;
        }

        /// <summary>
        /// Checks the tag.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private int CheckTag(int index, int size, string message)
        {
            int num1;
            if (index + 3 >= size || index + 4 >= size)
            {
                num1 = 0;
            }
            else if (message.Substring(index + 3, 1) == ":" || message.Substring(index + 4, 1) == ":")
            {
                if (CheckValidTag(index, message))
                {
                    int num2 = index;
                    _isTag = true;
                    return num2;
                }
                int swiftTag = message.GetNextSwiftTagIndex(index, _swiftTags);
                num1 = CheckTag(index + swiftTag, size, message);
                _isTag = false;
            }
            else
            {
                int swiftTag = message.GetNextSwiftTagIndex(index, _swiftTags);
                num1 = CheckTag(index + swiftTag, size, message);
                _isTag = false;
            }
            return num1;
        }

        /// <summary>
        /// Checks the valid tag.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private bool CheckValidTag(int index, string message) =>
            _swiftTags.Any(tag => message.Substring(index + 1, 2).Equals(tag) || message.Substring(index + 1, 3).Equals(tag));



        /// <summary>
        /// Loads the swift tags.
        /// </summary>
        private void LoadSwiftTags() => 
            _swiftTags = new List<string>
            {
                "11A",
                "11R",
                "11S",
                "12",
                "12A",
                "12B",
                "12C",
                "12E",
                "12F",
                "13A",
                "13B",
                "13C",
                "13D",
                "13E",
                "13J",
                "13K",
                "14A",
                "14C",
                "14D",
                "14F",
                "14G",
                "14J",
                "14S",
                "15A",
                "15B",
                "15C",
                "15D",
                "15E",
                "15F",
                "15G",
                "15H",
                "15I",
                "15J",
                "15K",
                "15L",
                "15M",
                "15N",
                "16A",
                "16R",
                "16S",
                "17A",
                "17B",
                "17F",
                "17G",
                "17N",
                "17O",
                "17R",
                "17T",
                "17U",
                "17V",
                "18A",
                "19",
                "19A",
                "19B",
                "20",
                "20C",
                "20D",
                "21",
                "21A",
                "21B",
                "21C",
                "21D",
                "21E",
                "21F",
                "21G",
                "21N",
                "21P",
                "21R",
                "22",
                "22A",
                "22B",
                "22C",
                "22D",
                "22E",
                "22F",
                "22G",
                "22H",
                "22J",
                "22K",
                "22X",
                "23",
                "23A",
                "23B",
                "23C",
                "23D",
                "23E",
                "23F",
                "23G",
                "24B",
                "24D",
                "25",
                "25A",
                "25D",
                "26A",
                "26B",
                "26C",
                "26D",
                "26E",
                "26F",
                "26H",
                "26N",
                "26P",
                "26T",
                "27",
                "28",
                "28C",
                "28D",
                "28E",
                "29A",
                "29B",
                "29C",
                "29E",
                "29F",
                "29G",
                "29H",
                "29J",
                "29K",
                "30",
                "30F",
                "30G",
                "30H",
                "30P",
                "30Q",
                "30T",
                "30U",
                "30V",
                "30X",
                "31C",
                "31D",
                "31E",
                "31F",
                "31G",
                "31L",
                "31P",
                "31R",
                "31S",
                "31X",
                "32A",
                "32B",
                "32C",
                "32D",
                "32E",
                "32F",
                "32G",
                "32H",
                "32J",
                "32K",
                "32M",
                "32N",
                "32P",
                "32Q",
                "32U",
                "33A",
                "33B",
                "33C",
                "33D",
                "33E",
                "33F",
                "33G",
                "33K",
                "33N",
                "33P",
                "33R",
                "33S",
                "33T",
                "34A",
                "34B",
                "34E",
                "34F",
                "34N",
                "34P",
                "34R",
                "35A",
                "35B",
                "35C",
                "35D",
                "35E",
                "35F",
                "35H",
                "35L",
                "35N",
                "35S",
                "35U",
                "36",
                "36A",
                "36B",
                "36C",
                "36E",
                "37A",
                "37B",
                "37C",
                "37D",
                "37E",
                "37F",
                "37G",
                "37H",
                "37J",
                "37K",
                "37L",
                "37M",
                "37N",
                "37P",
                "37R",
                "37U",
                "38A",
                "38B",
                "38D",
                "38E",
                "38G",
                "38H",
                "38J",
                "39A",
                "39B",
                "39C",
                "39P",
                "40A",
                "40B",
                "40C",
                "40E",
                "40F",
                "41A",
                "41D",
                "42A",
                "42C",
                "42D",
                "42M",
                "42P",
                "43P",
                "43T",
                "44A",
                "44B",
                "44C",
                "44D",
                "44E",
                "44F",
                "45A",
                "45B",
                "46A",
                "46B",
                "47A",
                "47B",
                "48",
                "49",
                "50",
                "50A",
                "50B",
                "50C",
                "50D",
                "50F",
                "50G",
                "50H",
                "50K",
                "50L",
                "51A",
                "51C",
                "51D",
                "52A",
                "52B",
                "52C",
                "52D",
                "52G",
                "53",
                "53A",
                "53B",
                "53C",
                "53D",
                "53J",
                "54",
                "54A",
                "54B",
                "54D",
                "55",
                "55A",
                "55B",
                "55D",
                "56",
                "56A",
                "56B",
                "56C",
                "56D",
                "56J",
                "57",
                "57A",
                "57B",
                "57C",
                "57D",
                "57J",
                "58A",
                "58B",
                "58C",
                "58D",
                "58J",
                "59",
                "59A",
                "59F",
                "60F",
                "60M",
                "61",
                "62F",
                "62M",
                "64",
                "65",
                "67A",
                "68A",
                "68B",
                "68C",
                "69A",
                "69B",
                "69C",
                "69D",
                "69E",
                "69F",
                "69J",
                "70",
                "70C",
                "70D",
                "70E",
                "70F",
                "70G",
                "71A",
                "71B",
                "71C",
                "71F",
                "71G",
                "71H",
                "71J",
                "71K",
                "71L",
                "72",
                "73",
                "74",
                "75",
                "76",
                "77A",
                "77B",
                "77D",
                "77E",
                "77F",
                "77G",
                "77H",
                "77J",
                "77T",
                "78",
                "79",
                "80C",
                "82A",
                "82B",
                "82D",
                "82J",
                "82S",
                "83A",
                "83C",
                "83D",
                "83J",
                "84A",
                "84B",
                "84D",
                "84J",
                "85A",
                "85B",
                "85D",
                "85J",
                "86",
                "86A",
                "86B",
                "86D",
                "86J",
                "87A",
                "87B",
                "87D",
                "87J",
                "90A",
                "90B",
                "90C",
                "90D",
                "90E",
                "90F",
                "90J",
                "91A",
                "91B",
                "91C",
                "91D",
                "91E",
                "91F",
                "91G",
                "91H",
                "92A",
                "92B",
                "92C",
                "92D",
                "92E",
                "92F",
                "92J",
                "92K",
                "92L",
                "92M",
                "92N",
                "93A",
                "93B",
                "93C",
                "93D",
                "94A",
                "94B",
                "94C",
                "94D",
                "94F",
                "94G",
                "95C",
                "95P",
                "95Q",
                "95R",
                "95S",
                "95T",
                "95U",
                "95V",
                "97A",
                "97B",
                "97C",
                "98A",
                "98B",
                "98C",
                "98D",
                "98E",
                "99A",
                "99B"
            };



    }
}
