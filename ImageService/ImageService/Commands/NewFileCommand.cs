using ImageService.Infrastructure;
using ImageService.Infrastructure.Event;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    /// <summary>
    /// A command for creating a new file in the system.
    /// </summary>
    public class NewFileCommand : ICommand
    {
        public EventHandler<LogChangedEventArgs> InformNewFile;

        private IImageServiceModal MyModal;
        /// <summary>
        /// Makes a new newfile command.
        /// </summary>
        /// <param name="modal">The image service modal that will actually add the file.</param>
        public NewFileCommand(IImageServiceModal modal)
        {
            MyModal = modal;            // Storing the Modal
        }
        /// <summary>
        /// Makes a new file in location args[0].
        /// </summary>
        /// <param name="args">The target location is in args[0].</param>
        /// <param name="result">True if the command was executed succsusfully, false otherwise.</param>
        /// <returns>The commands output.</returns>
        public string Execute(string[] args, out bool result)
        {
            // The String Will Return the New Path if result = true, and will return the error message
            return MyModal.AddFile(args[0], out result);
        }
    }
}
