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
    /*
     * a class for holding the amount of photos currently held in the output dir (not reading it but just counting
     *  the amount of photos moved since the start of server) also returns the students invloved in project
     */
    public class GetStatsCommand : ICommand
    {
        private int PhotoAmount;
        private string Name1;
        private string Name2;
        private string ID1;
        private string ID2;

        //TODO possibly get this shit done by reading from a file instead app config
        public GetStatsCommand()
        {
            PhotoAmount = 0;
            Name1 = ConfigurationManager.AppSettings["Name1"];
            Name2 = ConfigurationManager.AppSettings["Name2"];
            ID1 = ConfigurationManager.AppSettings["ID1"];
            ID2 = ConfigurationManager.AppSettings["ID2"];
        }

        public void PhotoAdded(object sender, LogChangedEventArgs args)
        {
            PhotoAmount++;
        }

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
