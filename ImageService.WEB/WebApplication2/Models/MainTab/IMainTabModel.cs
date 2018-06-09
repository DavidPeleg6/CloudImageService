using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models.MainTab
{
    /// <summary>
    /// Interface for the main tab model.
    /// </summary>
    interface IMainTabModel
    {
        int ImageCount { get; set; }
        string Name1 { get; set; }
        string Name2 { get; set; }
        string ID1 { get; set; }
        string ID2 { get; set; }
    }
}
