using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        private static Regex r = new Regex(":");

        public string AddFile(string path, out bool result)
        {
            //first check if file exists
            if(!System.IO.File.Exists(path))
            {
                result = false;
                return "AddFile error: File does not exist.";
            }
            // top-level folder name.
            string outputPath = ConfigurationManager.AppSettings["OutputDir"];
            DateTime date = GetDateTakenFromImage(path);
            //parse date into path for a new folder
            string time = date.Year.ToString() + @"\" + date.Month.ToString();
            outputPath = System.IO.Path.Combine(outputPath, time);
            if (!System.IO.Directory.Exists(outputPath))
            {
                System.IO.Directory.CreateDirectory(outputPath);
            }
            //get file name to update path for copying
            string fileName = System.IO.Path.GetFileName(path);
            string targetFile = System.IO.Path.Combine(outputPath, fileName);
            System.IO.File.Copy(path, targetFile);
            result = true;
            return outputPath;
        }

        //retrieves the datetime WITHOUT loading the whole image
        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }
    }
}
