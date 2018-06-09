using ImageService.Commands;
using ImageService.Infrastructure.Event;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageService.Commands
{
    /// <summary>
    /// A command that returns the stats the main window of the website needs.
    /// I.E the number of images the service has processed thus far and the personal information of the developers.
    /// The number of images is maintained by counting every time a new image is processed.
    /// The student information is read from the app.config file.
    /// </summary>
    public class GetStatsCommand : ICommand
    {
        private int PhotoAmount;
        private string Name1;
        private string Name2;
        private string ID1;
        private string ID2;
        
        /// <summary>
        /// Constructor, sets the ammount of photos to 0 (since this constructor is called as the service is launched).
        /// and load the student information from app.config.
        /// </summary>
        public GetStatsCommand()
        {
            PhotoAmount = 0;
            Name1 = ConfigurationManager.AppSettings["Name1"];
            Name2 = ConfigurationManager.AppSettings["Name2"];
            ID1 = ConfigurationManager.AppSettings["ID1"];
            ID2 = ConfigurationManager.AppSettings["ID2"];
        }
        /// <summary>
        /// An event that is called when the service processes an image.
        /// Increments the photo ammount counter.
        /// </summary>
        /// <param name="sender">The service probably, not used</param>
        /// <param name="args">Also not used</param>
        public void PhotoAdded(object sender, LogChangedEventArgs args)
        {
            PhotoAmount++;
        }
        /// <summary>
        /// Executes the command, placing the information it is supposed to return in a JSON object
        /// and returning said JSON object.
        /// </summary>
        /// <param name="args">not used</param>
        /// <param name="result">not used</param>
        /// <returns>The number of images and the student information, in JSON format.</returns>
        public string Execute(string[] args, out bool result)
        {
            result = true;
            JObject JSONStatObject = new JObject();
            JSONStatObject["type"] = "stats";
            JSONStatObject["photo_amount"] = PhotoAmount;
            JSONStatObject["Name1"] = Name1;
            JSONStatObject["Name2"] = Name2;
            JSONStatObject["ID1"] = ID1;
            JSONStatObject["ID2"] = ID2;
            return JSONStatObject.ToString();
        }
    }
}
