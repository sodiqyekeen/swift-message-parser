using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftMessageParser.Entities.MT;
using SwiftMessageParser.Entities.Tags;
using SwiftMessageParser.Extensions;

namespace SwiftMessageParser.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class SwiftMessage
    {
        public SwiftMessage(string message)
        {
            RawText = message;
            if (message.Contains("{1:")) BasicHeader = new BasicHeader(message.Between("{1:", "}"));

            if (message.Contains("{2:")) ApplicationHeader = new ApplicationHeader(message.Between("{2:", "}"));

            if (message.Contains("{3:")) UserHeader = new UserHeader(message.Between("{3:", "{4:"));

            if (message.Contains("{4:")) ParseBody(message);

            if (message.Contains("{5:")) Trailer = new Trailer(message.Between("{5:{", "}"));
        }

        /// <summary>
        /// Gets the raw text.
        /// </summary>
        /// <value>
        /// The raw text.
        /// </value>
        public string RawText { get; private set; }

        /// <summary>
        /// Gets the message type <see cref="MessageTypes"/>.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public MessageTypes Type => ApplicationHeader.MessageType switch
        {
            "103" => MessageTypes.MT103,
            "910" => MessageTypes.MT910,
            "942" => MessageTypes.MT942,
            _ => throw new ArgumentException(Exceptions.InvalidType)
        };

        /// <summary>
        /// Gets or sets the basic header.
        /// </summary>
        /// <value>
        /// The basic header.
        /// </value>
        public BasicHeader BasicHeader { get; set; }

        /// <summary>
        /// Gets or sets the application header.
        /// </summary>
        /// <value>
        /// The application header.
        /// </value>
        public ApplicationHeader ApplicationHeader { get; set; }

        /// <summary>
        /// Gets or sets the user header.
        /// </summary>
        /// <value>
        /// The user header.
        /// </value>
        public UserHeader UserHeader { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public List<ITag> Body { get; set; }

        /// <summary>
        /// Gets or sets the trailer.
        /// </summary>
        /// <value>
        /// The trailer.
        /// </value>
        public Trailer Trailer { get; set; }

        /// <summary>
        /// Parses the swift message to its exact message type <see cref="MessageTypes"/>.
        /// </summary>
        /// <returns></returns>
        public IMessageType ToExact()
        {
            var message = Type.GetParser();
            message.Parse(this);
            return message;
        }

        private void ParseBody(string message)
        {
            string str = message.Between("{4:", "}");
            Body = new List<ITag>();
            MTParser mtParser = new MTParser();
            List<string> temp = new List<string>();
            if (ApplicationHeader != null && ApplicationHeader.MessageType.Equals("942"))
            {
                List<string> filteredString = str.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                filteredString = filteredString.Where(x => x.StartsWith(":") && !x.Contains(":86:")).ToList();
                temp = mtParser.Block4ToList(string.Join(Environment.NewLine, filteredString));
            }
            else
            {
                temp = mtParser.Block4ToList(str);
            }
            TagFactory tagFactory = new TagFactory();
            temp.ForEach(item => Body = tagFactory.CreateInstance(item, Body));
        }      
    }
}
