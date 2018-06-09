using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.WEB.Models
{
    /// <summary>
    /// Interface for the main tab model.
    /// Contains a list of items the model should be able to return.
    /// </summary>
    interface IMainTabModel
    {
        bool Running { get; }
        int ImageCount { get; set; }
        string Name1 { get; set; }
        string Name2 { get; set; }
        string ID1 { get; set; }
        string ID2 { get; set; }
    }
}
