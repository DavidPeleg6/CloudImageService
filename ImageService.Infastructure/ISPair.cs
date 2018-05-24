using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Infastructure
{
    /// <summary>
    /// A log message and its type, used to easy pass logs around.
    /// </summary>
    public class ISPair
    {
        public string Message { get; set; }
        public MessageTypeEnum Type { get; set; }
        /// <summary>
        /// Constructor, makes an ISPair.
        /// </summary>
        /// <param name="type">The type of log.</param>
        /// <param name="msg">The log itself.</param>
        public ISPair(MessageTypeEnum type, string msg)
        {
            Message = msg;
            Type = type;
        }
    }
}
