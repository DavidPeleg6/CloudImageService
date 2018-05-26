using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageService.GUI
{
    /// <summary>
    /// This class is used to pass commands between the view and viewmodel,
    /// which then calls upon the relevant functions in the model.
    /// I used it to make buttons do things (by binding them to commands).
    /// I copied this class from stack overflow.
    /// (I added the documentation myself).
    /// </summary>
    class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        /// <summary>
        /// Constructor, sets the action that the command will perform and also if it can.
        /// </summary>
        /// <param name="action">the action that the command will perform</param>
        /// <param name="canExecute">if it can</param>
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }
        /// <summary>
        /// A property specifing if the command can execute or not.
        /// </summary>
        /// <param name="parameter">Something that could be used to detrmine if execution is possible, isn't used.</param>
        /// <returns>Wrather the command can execute or not.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// Executes the command, just executes its action.
        /// </summary>
        /// <param name="parameter">Something that could modify execution, isn't used.</param>
        public void Execute(object parameter)
        {
            _action();
        }
    }
}
