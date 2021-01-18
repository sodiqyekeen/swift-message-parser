using System;
using System.Collections.Generic;
using System.Linq;
using SwiftMessageParser.Entities.Tags;
using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities.MT
{
    public class MT103 : IMessageType
    {
        /// <summary>
        /// Gets or sets the uetr.
        /// </summary>
        /// <value>
        /// The uetr.
        /// </value>
        public string Uetr { get; set; }

        /// <summary>
        /// Gets or sets the sender bank bic.
        /// </summary>
        /// <value>
        /// The sender bank bic.
        /// </value>
        public string SenderBankBic { get; set; }

        /// <summary>
        /// Gets or sets the sender reference.
        /// </summary>
        /// <value>
        /// The sender reference.
        /// </value>
        public string SenderReference { get; set; }

        /// <summary>
        /// Gets or sets the bank operation code.
        /// </summary>
        /// <value>
        /// The bank operation code.
        /// </value>
        public string BankOperationCode { get; set; }

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
        /// Gets or sets the beneficiary account.
        /// </summary>
        /// <value>
        /// The beneficiary account.
        /// </value>
        public string BeneficiaryAccount { get; set; }

        /// <summary>
        /// Gets or sets the name of the beneficiary.
        /// </summary>
        /// <value>
        /// The name of the beneficiary.
        /// </value>
        public string BeneficiaryName { get; set; }

        /// <summary>
        /// Gets or sets the remittance information.
        /// </summary>
        /// <value>
        /// The remittance information.
        /// </value>
        public string RemittanceInfo { get; set; }

        /// <summary>
        /// Gets or sets the details of charges.
        /// </summary>
        /// <value>
        /// The details of charges.
        /// </value>
        public string DetailsOfCharges { get; set; }

        /// <summary>
        /// Gets or sets the sender charges.
        /// </summary>
        /// <value>
        /// The sender charges.
        /// </value>
        public string SenderCharges { get; set; }

        public string RawText { get; private set; }
        /// <summary>
        /// Parses the MT103.
        /// </summary>
        /// <param name="mt103Message">The MT103 message.</param>
        public void Parse(SwiftMessage mt103Message)
        {
            RawText = mt103Message.RawText;
            SenderBankBic = mt103Message.ApplicationHeader.SenderBIC ?? "";
            Uetr = mt103Message.UserHeader.Uetr ?? "";
            ParseBlock4(mt103Message.Body);
        }

        /// <summary>
        /// Parses the block4.
        /// </summary>
        /// <param name="block4">The block4.</param>
        private void ParseBlock4(List<ITag> block4)
        {
            var field20 = block4.Where(x => x.TagName == "20").FirstOrDefault();

            if (field20 != null) ParseField20(field20);

            var field23B = block4.Where(x => x.TagName == "23B").FirstOrDefault();
            if (field23B != null) ParseField23B(field23B);

            var field32A = block4.Where(x => x.TagName == "32A").FirstOrDefault();
            if (field32A != null) ParseField32A(field32A);

            var field50 = block4.Where(x => x.TagName == "50A" || x.TagName == "50F" || x.TagName == "50K").FirstOrDefault();
            if (field50 != null) ParseField50(field50);

            var field52 = block4.Where(x => x.TagName == "52A" || x.TagName == "52D").FirstOrDefault();
            if (field52 != null) ParseField52(field52);

            var field59 = block4.Where(x => x.TagName == "59" || x.TagName == "59A" || x.TagName == "59F").FirstOrDefault();
            if (field59 != null) ParseField59(field59);

            var field70 = block4.Where(x => x.TagName == "70").FirstOrDefault();
            if (field70 != null) ParseField70(field70);

            var field71A = block4.Where(x => x.TagName == "71A").FirstOrDefault();
            if (field71A != null) ParseField71A(field71A);

            var field71F = block4.Where(x => x.TagName == "71F").ToList();
            if (field71F != null) ParseField71F(field71F);
        }

        /// <summary>
        /// Parses the field20.
        /// </summary>
        /// <param name="field20">The field20.</param>
        private void ParseField20(ITag field20) =>
            SenderReference = field20.Value ?? "";

        /// <summary>
        /// Parses the field23 b.
        /// </summary>
        /// <param name="field23B">The field23B.</param>
        private void ParseField23B(ITag field23B) =>
            BankOperationCode = field23B.Value ?? "";

        /// <summary>
        /// Parses the field52.
        /// </summary>
        /// <param name="field52">The field52.</param>
        private void ParseField52(ITag field52) =>
            SendingBank = field52.Value ?? "";

        /// <summary>
        /// Parses the field70.
        /// </summary>
        /// <param name="field70">The field70.</param>
        private void ParseField70(ITag field70) =>
            RemittanceInfo = field70.Value ?? "";

        /// <summary>
        /// Parses the field71 a.
        /// </summary>
        /// <param name="field71A">The field71A.</param>
        private void ParseField71A(ITag field71A) =>
            DetailsOfCharges = field71A.Value ?? "";

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
        private void ParseField50(ITag field50) =>
            OrderingCustomer = $"{field50.Value ?? ""} {field50.Qualifier ?? ""} {field50.Description ?? ""}";

        /// <summary>
        /// Parses the field59.
        /// </summary>
        /// <param name="field59">The field59.</param>
        private void ParseField59(ITag field59)
        {
            BeneficiaryAccount = field59.Value.RemoveSpecialCharactersAndLetter();
            BeneficiaryName = field59.Qualifier;
        }

        /// <summary>
        /// Parses the field71 f.
        /// </summary>
        /// <param name="field71F">The field71f.</param>
        private void ParseField71F(List<ITag> field71F)
        {
            string _senderCharges = string.Empty;
            foreach (var item in field71F)
                _senderCharges = string.IsNullOrEmpty(_senderCharges) ?
                    $"{item.Code ?? ""}{item.Value ?? ""}" :
                    $"{_senderCharges}/{item.Code ?? ""}{item.Value ?? ""}";

            SenderCharges = _senderCharges;
        }
    }
}
