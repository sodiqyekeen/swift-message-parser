using System;
using System.Linq;
using SwiftMessageParser.Entities.Tags;
using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.MT
{
    public class MT910 : IMessageType
    {
        /// <summary>
        /// Gets or sets the sender reference.
        /// </summary>
        /// <value>
        /// The sender reference.
        /// </value>
        public string SenderReference { get; set; }

        /// <summary>
        /// Gets or sets the related reference.
        /// </summary>
        /// <value>
        /// The related reference.
        /// </value>
        public string RelatedReference { get; set; }

        /// <summary>
        /// Gets or sets the account identification.
        /// </summary>
        /// <value>
        /// The account identification.
        /// </value>
        public string AccountIdentification { get; set; }

        /// <summary>
        /// Gets or sets the interbank settled amount.
        /// </summary>
        /// <value>
        /// The interbank settled amount.
        /// </value>
        public decimal? InterbankSettledAmount { get; set; }

        /// <summary>
        /// Gets or sets the value date.
        /// </summary>
        /// <value>
        /// The value date.
        /// </value>
        public DateTime? ValueDate { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the ordering customer.
        /// </summary>
        /// <value>
        /// The ordering customer.
        /// </value>
        public string OrderingCustomer { get; set; }

        /// <summary>
        /// Gets or sets the sending bank.
        /// </summary>
        /// <value>
        /// The sending bank.
        /// </value>
        public string SendingBank { get; set; }

        /// <summary>
        /// Gets or sets the sender bank bic.
        /// </summary>
        /// <value>
        /// The sender bank bic.
        /// </value>
        public string SenderBankBic { get; set; }

        public string RawText { get; private set; }
        /// <summary>
        /// Parses the MT910.
        /// </summary>
        /// <param name="mt910Message">The MT910 message.</param>
        public void Parse(SwiftMessage mt910Message)
        {
            RawText = mt910Message.RawText;
            SenderBankBic = mt910Message.ApplicationHeader.SenderBIC ?? "";

            var field20 = mt910Message.Body.Where(x => x.TagName == "20").FirstOrDefault();
            if (field20 != null) ParseField20(field20);

            var field21 = mt910Message.Body.Where(x => x.TagName == "21").FirstOrDefault();
            if (field21 != null) ParseField21(field21);

            var field25 = mt910Message.Body.Where(x => x.TagName == "25").FirstOrDefault();
            if (field25 != null) ParseField25(field25);

            var field32A = mt910Message.Body.Where(x => x.TagName == "32A").FirstOrDefault();
            if (field32A != null) ParseField32A(field32A);

            var field50 = mt910Message.Body.Where(x => x.TagName == "50A" || x.TagName == "50F" || x.TagName == "50K").FirstOrDefault();
            if (field50 != null) ParseField50(field50);

            var field52 = mt910Message.Body.Where(x => x.TagName == "52A" || x.TagName == "52D").FirstOrDefault();
            if (field52 != null) ParseField52(field52);
        }

        public override string ToString()
        {
            return $"SenderReference:{SenderReference}\n" +
                $"RelatedReference:{RelatedReference}\n" +
                $"AccountIdentification:{AccountIdentification}\n" +
                $"InterbankSettledAmount:{InterbankSettledAmount}\n" +
                $"ValueDate:{ValueDate}\n" +
                $"OrderingCustomer:{OrderingCustomer}\n" +
                $"SendingBank:{SendingBank}\n" +
                $"SenderBankBic:{SenderBankBic}\n";
        }

        /// <summary>
        /// Parses the field20.
        /// </summary>
        /// <param name="field20">The field20.</param>
        private void ParseField20(ITag field20) => SenderReference = field20.Value ?? "";

        /// <summary>
        /// Parses the field21.
        /// </summary>
        /// <param name="field21">The field21.</param>
        private void ParseField21(ITag field21) => RelatedReference = field21.Value ?? "";

        /// <summary>
        /// Parses the field25.
        /// </summary>
        /// <param name="field25">The field25.</param>
        private void ParseField25(ITag field25) => AccountIdentification = field25.Value ?? "";

        /// <summary>
        /// Parses the field32 a.
        /// </summary>
        /// <param name="field32A">The field32 a.</param>
        private void ParseField32A(ITag field32A)
        {
            ValueDate = field32A.Qualifier.CovertToDate("yyMMdd");
            Currency = field32A.Code ?? "";
            InterbankSettledAmount = Convert.ToDecimal(field32A.Value.Replace(',', '.'));
        }

        /// <summary>
        /// Parses the field50.
        /// </summary>
        /// <param name="field50">The field50.</param>
        private void ParseField50(ITag field50) => OrderingCustomer = $"{field50.Value ?? ""} {field50.Qualifier ?? ""} {field50.Description ?? ""}";

        /// <summary>
        /// Parses the field52.
        /// </summary>
        /// <param name="field52">The field52.</param>
        private void ParseField52(ITag field52) => SendingBank = field52.Value ?? "";

    }
}
