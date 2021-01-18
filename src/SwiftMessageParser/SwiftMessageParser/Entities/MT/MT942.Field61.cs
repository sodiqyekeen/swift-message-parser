using System;

namespace SwiftMessageParser.Entities.MT
{
    public class Field61
    {
        public string TransactionReference { get; set; }

        /// <summary>
        /// Gets or sets the related reference.
        /// </summary>
        /// <value>
        /// The related reference.
        /// </value>
        public string RelatedReference { get; set; }

        /// <summary>
        /// Gets or sets the value date.
        /// </summary>
        /// <value>
        /// The value date.
        /// </value>
        public DateTime? ValueDate { get; set; }

        /// <summary>
        /// Gets or sets the interbank settled amount.
        /// </summary>
        /// <value>
        /// The interbank settled amount.
        /// </value>
        public decimal? InterbankSettledAmount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }
    }
}
