using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ImageService.WEB.Models
{
    /// <summary>
    /// A class used to pass handelers to the view.
    /// Contains just the adress of the handeler.
    /// </summary>
    public class HandelerData
    {
        static int Count = 0;
        /// <summary>
        /// Contructor, makes an empty handeler with a uniqe ID.
        /// </summary>
        public HandelerData()
        {
            Count++;
            ID = Count;
        }
        /// <summary>
        /// Copies the contents of handelerData to this HandelerData
        /// </summary>
        /// <param name="handelerData">HandelerData whose contents will by copied to this HandelerData.</param>
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