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
    /// <summary>
    /// A class that implemetns the ImageServiceModal interface.
    /// </summary>
    public class ImageServiceModal : IImageServiceModal
    {
        private static Regex RegularExpression = new Regex(":");
        /// <summary>
        /// The Function Addes A file to the system
        /// </summary>
        /// <param name="path">The Path of the Image from the file</param>
        /// <returns>Indication if the Addition Was Successful</returns>
        public string AddFile(string path, out bool result)
        {
            //first check if file exists
            if(!System.IO.File.Exists(path))
            {
                result = false;
                return "AddFile error: File does not exist.";
            }
            // top-level folder name.
            string OutputPath = ConfigurationManager.AppSettings["OutputDir"];
            DateTime Date = GetDateTakenFromImage(path);
            //parse date into path for a new folder
            string Time = Date.Year.ToString() + @"\" + Date.Month.ToString();
            OutputPath = System.IO.Path.Combine(OutputPath, Time);
            if (!System.IO.Directory.Exists(OutputPath))
            {
                System.IO.Directory.CreateDirectory(OutputPath);
            }
            //get file name to update path for copying
            string FileName = System.IO.Path.GetFileName(path);
            string TargetFile = System.IO.Path.Combine(OutputPath, FileName);
            System.IO.File.Copy(path, TargetFile);
            result = true;
            return OutputPath;
        }

        /// <summary>
        /// A function that retrieves the datetime WITHOUT loading the whole image.
        /// </summary>
        /// <param name="path">The path to the image.</param>
        /// <returns></returns>
        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream ImageFileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image MyImage = Image.FromStream(ImageFileStream, false, false))
            {
                PropertyItem PropItem = MyImage.GetPropertyItem(36867);
                string DateTaken = RegularExpression.Replace(Encoding.UTF8.GetString(PropItem.Value), "-", 2);
                return DateTime.Parse(DateTaken);
            }
        }
    }
}
