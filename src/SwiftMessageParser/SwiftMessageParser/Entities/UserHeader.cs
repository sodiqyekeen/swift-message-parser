using SwiftMessageParser.Extensions;
using System.Collections.Generic;

namespace SwiftMessageParser.Entities
{

  public class UserHeader
  {

        /// <summary>
        /// Gets or sets the uetr.
        /// </summary>
        /// <value>
        /// The uetr.
        /// </value>
        public string Uetr { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserHeader"/> class.
        /// </summary>
        public UserHeader()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserHeader"/> class.
        /// </summary>
        /// <param name="parsedSwiftMessage">The parsed swift message.</param>
        public UserHeader(Dictionary<string, string> parsedSwiftMessage)
        {
            string str = parsedSwiftMessage[nameof(UserHeader)];
            if (str.Contains("{121:"))
                Uetr = str.Between("{121:", "}");
            else
                Uetr = "";
        }
        
        public UserHeader(string str)
        {
            if (str.Contains("{121:"))
                Uetr = str.Between("{121:", "}");
            else
                Uetr = "";
        }
    }
}
