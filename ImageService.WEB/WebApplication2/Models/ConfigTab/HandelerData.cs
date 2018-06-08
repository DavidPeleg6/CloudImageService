using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ImageService.WEB.Models
{
    /// <summary>
    /// Contains all data that the config page is supposed to display.
    /// The first four are Output Directory, Source Name, 
    /// </summary>
    public class HandelerData
    {
        static int count = 0;
        public HandelerData()
        {
            count++;
            ID = count;
        }
        public void copy(HandelerData handelerData)
        {
            Text = handelerData.Text;
        }
        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Text")]
        public string Text { get; set; }
    }
}