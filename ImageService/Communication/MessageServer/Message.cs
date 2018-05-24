using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.MessageServer
{
    /// <summary>
    /// A message that is passed by the server.
    /// Can be set to be a message that is being passed to and from the server using its boolean properties.
    /// </summary>
    public class Message
    {
        public bool ReadMessage { get; set; }
        public int AmountWrote { get; set; }
        public string MessageString { get; set; }
        public bool WriteMessage { get; set; }
        /// <summary>
        /// Constructor, simply sets that the messages values to the ones given to it.
        /// </summary>
        /// <param name="messagePending">A boolean specifing if the message is to be readen.</param>
        /// <param name="writeMessage">A boolean specifing if the message is to be written.</param>
        /// <param name="message">The message itself.</param>
        public Message(bool messagePending, bool writeMessage, string message)
        {
            ReadMessage = messagePending;
            WriteMessage = writeMessage;
            MessageString = message;
            AmountWrote = 0;
        }

    }
}
