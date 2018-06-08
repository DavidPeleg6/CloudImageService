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
        private int photo_amount;
        private Dictionary<string, string> stats;

        //TODO possibly get this shit done by reading from a file instead app config
        public GetStatsCommand()
        {
            photo_amount = 0;
            stats = new Dictionary<string, string>()
            {
                { ConfigurationManager.AppSettings["Name1"], ConfigurationManager.AppSettings["ID1"] },
                { ConfigurationManager.AppSettings["Name2"], ConfigurationManager.AppSettings["ID2"] }
            };
        }

        public void PhotoAdded(object sender, LogChangedEventArgs args)
        {
            photo_amount++;
        }

        public string Execute(string[] args, out bool result)
        {
            result = true;
            JObject JSONStatObject = new JObject();
            JSONStatObject["Dict"] = JsonConvert.SerializeObject(stats, Formatting.Indented);
            JSONStatObject["photo_amount"] = photo_amount;
            return JSONStatObject.ToString();
        }
    }
}
