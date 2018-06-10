using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageService.WEB.Models
{
    public class PhotoTabModel
    {
        string output_dir;
        List<string> photos;

        /// <summary>
        /// constructor of the class, creates a new list of strings for representing photos
        /// </summary>
        /// <param name="output_dir">directory in which photos are wirtten (we presume it is local)</param>
        public PhotoTabModel(string output_dir = "unnable to connect to server")
        {
            photos = new List<string>();
            this.output_dir = output_dir;
        }
        
        /// <summary>
        /// the method to delete an image
        /// </summary>
        /// <param name="RelAddress">the address of the root (the local address)</param>
        /// <param name="year">year photo is taken</param>
        /// <param name="month">month photo is taken</param>
        /// <param name="name">photo name</param>
        public void DeleteImage(string RelAddress, string year, string month, string name)
        {
            string[] path = { RelAddress, year, month, name };
            string Address = Path.Combine(path);
            File.Delete(Address);
        }
        
        /// <summary>
        /// a method for getting all photos and put into a list of strings
        /// </summary>
        /// <param name="output_dir">the root for everything</param>
        /// <returns>a list of strings</returns>
        public List<string> PhotoAddresses(string output_dir)
        {
            List<string> photos = new List<string>();
            foreach (string dir_year in Directory.EnumerateDirectories(output_dir))
            {
                foreach (string dir_month in Directory.EnumerateDirectories(dir_year))
                {
                    foreach (string photo in Directory.EnumerateFiles(dir_month))
                    {
                        photos.Add(photo);
                    }
                }
            }
            return photos;
        }
    }
}