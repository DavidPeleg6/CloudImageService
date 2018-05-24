using ImageService.Infastructure;
using ImageService.Logging.Modal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ImageService.Infrastructure.Event
{
    /// <summary>
    /// These event args are used in detail specification when a log is added.
    /// </summary>
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
            Message = message;              // Storing the String
        }
        /// <summary>
        /// Formatted a LogChangedEventArgs to a JSON format.
        /// </summary>
        /// <param name="e">The object to be formatted.</param>
        /// <returns>The formatted object.</returns>
        public static string LogChangeToJSON(LogChangedEventArgs e)
        {
            JObject JSONLogObject = new JObject();
            JSONLogObject["type: "] = (int)e.Type;
            JSONLogObject["message: "] = e.Message;
            JSONLogObject["type"] = "log";
            return JSONLogObject.ToString();
        }
        /// <summary>
        /// Formatts a complete list of logs to JSON format.
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static string CompleteLogToJSON(List<ISPair> log)
        {
            JObject JSONLogListObject = new JObject();
            JSONLogListObject["Dict"] = JsonConvert.SerializeObject(log, Formatting.Indented);
            JSONLogListObject["type"] = "log";
            return JSONLogListObject.ToString();
        }
    }
}
