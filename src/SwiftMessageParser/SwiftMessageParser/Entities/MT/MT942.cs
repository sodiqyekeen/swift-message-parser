using System;
using System.Collections.Generic;
using System.Linq;
using SwiftMessageParser.Entities.Tags;
using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.MT
{
    public class MT942 : IMessageType
    {
        /// <summary>
        /// Gets or sets the sender reference.
        /// </summary>
        /// <value>
        /// The sender reference.
        /// </value>
        public string SenderReference { get; set; }

        /// <summary>
        /// Gets or sets the account identification.
        /// </summary>
        /// <value>
        /// The account identification.
        /// </value>
        public string AccountIdentification { get; set; }

        /// <summary>
        /// Gets or sets the field61s.
        /// </summary>
        /// <value>
        /// The field61s.
        /// </value>
        public List<Field61> Field61s { get; set; }

        public string RawText { get; private set; }

        /// <summary>
        /// Parses the MT942.
        /// </summary>
        /// <param name="mt942Message">The MT942 message.</param>
        public void Parse(SwiftMessage mt942Message)
        {
            RawText = mt942Message.RawText;
            var field20 = mt942Message.Body.Where(x => x.TagName == "20").FirstOrDefault();
            if (field20 != null) ParseField20(field20);

            var field25 = mt942Message.Body.Where(x => x.TagName == "25").FirstOrDefault();
            if (field25 != null) ParseField25(field25);

            //var field34F = mt942Message.Body.Where(x => x.TagName == "34F").FirstOrDefault();
            //if (field34F != null)
            //{
            //    ParseField25(field25);
            //}

            var field61 = mt942Message.Body.Where(x => x.TagName == "61").ToList();
            if (field61 != null) ParseField61F(field61);
        }

        /// <summary>
        /// Parses the field61 f.
        /// </summary>
        /// <param name="field61Tags">The field61.</param>
        private void ParseField61F(List<ITag> field61Tags) =>
            Field61s = field61Tags.Where(field61Tag => !field61Tag.Qualifier.Before(",").Contains("DD")).Select(field61Tag => ExtractField61Detail(field61Tag)).ToList();


        private static Field61 ExtractField61Detail(ITag field61Tag)
        {
            Field61 field61Object = new Field61
            {
                RelatedReference = field61Tag.Value ?? ""
            };
            string _tempString = field61Tag.Qualifier;
            field61Object.TransactionReference = _tempString.After("NTRF");
            field61Object.ValueDate = _tempString.Substring(0, 6).CovertToDate("yyMMdd");

            string operationCode = string.Empty;
            if (_tempString.Contains("CD"))
            {
                operationCode = "CD";
                field61Object.Currency = "USD";
            }
            else if (_tempString.Contains("CR"))
            {
                operationCode = "CR";
                field61Object.Currency = "EUR";
            }
            else if (_tempString.Contains("CP"))
            {
                operationCode = "CP";
                field61Object.Currency = "GBP";
            }

            if (!string.IsNullOrEmpty(operationCode))
                field61Object.InterbankSettledAmount = Convert.ToDecimal(_tempString.AmountFromField61(operationCode, ","));
            return field61Object;
        }

        /// <summary>
        /// Parses the field20.
        /// </summary>
        /// <param name="field20">The field20.</param>
        private void ParseField20(ITag field20) => SenderReference = field20.Value ?? "";

        /// <summary>
        /// Parses the field25.
        /// </summary>
        /// <param name="field25">The field25.</param>
        private void ParseField25(ITag field25) => AccountIdentification = field25.Value ?? "";
    }
}
