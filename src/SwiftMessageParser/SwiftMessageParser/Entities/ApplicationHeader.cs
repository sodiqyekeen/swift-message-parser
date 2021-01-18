namespace SwiftMessageParser.Entities
{
    public class ApplicationHeader
    {
        /// <summary>
        /// Gets or sets the swift direction.
        /// </summary>
        /// <value>
        /// The swift direction.
        /// </value>
        public string SwiftDirection { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>
        /// The type of the message.
        /// </value>
        public string MessageType { get; set; }

        /// <summary>
        /// Gets or sets the sender bic.
        /// </summary>
        /// <value>
        /// The sender bic.
        /// </value>
        public string SenderBIC { get; set; }

        /// <summary>
        /// Gets or sets the branch code.
        /// </summary>
        /// <value>
        /// The branch code.
        /// </value>
        public string BranchCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationHeader"/> class.
        /// </summary>
        /// <param name="str">The string.</param>
        public ApplicationHeader(string str)
        {
            SwiftDirection = str.Substring(0, 1);
            MessageType = str.Substring(1, 3);
            SenderBIC = str.Length < 24 ? str.Substring(4, 8) : str.Substring(14, 8);
        }
    }
}
