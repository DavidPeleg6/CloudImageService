using ImageService.Logging.Modal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ImageService.Infrastructure.Event
{
    public class LogChangedEventArgs
    {
        public MessageTypeEnum Type { get; set; }       // The path to the directory.

        public string Message { get; set; }             // The Message That went to the logger

        /// <summary>
        /// Contructs a new DirectoryCloseEventArgs object.
        /// </summary>
        /// <param name="dirPath">The directory name and path to be stored.</param>
        /// <param name="message">The messege to be stored.</param>
        public LogChangedEventArgs(string message, MessageTypeEnum type)
        {
            Type = type;                    // Setting the Directory Name
            Message = message;                          // Storing the String
        }

        public static string LogChangeToJSON(LogChangedEventArgs e)
        {
            JObject log_obj = new JObject();
            log_obj["type: "] = (int)e.Type;
            log_obj["message: "] = e.Message;
            return log_obj.ToString();
        }

        public static string CompleteLogToJSON(Dictionary<int, string> log)
        {
            return JsonConvert.SerializeObject(log, Formatting.Indented);
        }
    }
}
