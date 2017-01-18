using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MagicWordCounter.Core
{
    public class RelayCommand : ICommand
    {

        /// <summary>
        /// Creates a new instance of <see cref="RelayCommand"/>
        /// </summary>
        /// <param name="executeAction">Action to be invoked upon execution</param>
        /// <param name="canExecuteAction">Function that determines if the command can be executed.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="executeAction"/> or <paramref name="canExecuteAction"/> is null.</exception>
        public RelayCommand(Action executeAction, Func<bool> canExecuteAction)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException(nameof(executeAction));
            }
            if (canExecuteAction == null)
            {
                throw new ArgumentNullException(nameof(canExecuteAction));
            }

            this.ExecuteAction = executeAction;
            this.CanExecuteAction = canExecuteAction;
        }

        public event EventHandler CanExecuteChanged;

        protected Func<bool> CanExecuteAction { get; set; }

        protected Action ExecuteAction { get; set; }

        public bool CanExecute(object parameter)
        {
            return CanExecuteAction.Invoke();
        }

        public void Execute(object parameter)
        {
            ExecuteAction.Invoke();
        }
    }
}

