using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DotNetLead.DragDrop.UI.Common
{
    public class CommandBase : ICommand
    {
        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        public CommandBase(Action<object> executeDelegate, Predicate<object> canExecuteDelegate)
        {
            execute = executeDelegate;
            canExecute = canExecuteDelegate;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        void ICommand.Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
