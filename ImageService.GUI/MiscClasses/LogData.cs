using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ImageService.Logging.Modal;

namespace ImageService.GUI
{
    /// <summary>
    /// Log data, a class used by the logs window to conviniently store log data.
    /// </summary>
    class LogData
    {
        private int type_num;
        /// <summary>
        /// constructor, makes a logdata object. only keeps the type if its valid (between 0 and 2).
        /// Sets it to 0 otherwise.
        /// </summary>
        /// <param name="type">The type of message, can be INFO, WARNING or ERROR.</param>
        /// <param name="message">The log message itself.</param>
        public LogData(MessageTypeEnum type, string message)
        {
            if ((int)type <= 2 && 0 <= (int)type)
                type_num = (int)type;
            else
                type_num = -1;
            this.Message = message;
        }
        /// <summary>
        /// The type of log as a string, for GUI display purposes.
        /// </summary>
        public string Type {

            get
            {
                switch (type_num)
                {
                    case (int)MessageTypeEnum.INFO: return "INFO";
                    case (int)MessageTypeEnum.FAIL: return "ERROR";
                    case (int)MessageTypeEnum.WARNING: return "WARNING";
                    default: return "DISPLAY ERROR";
                }
            }
        }
        public string Message { get; set; }
    }
}
