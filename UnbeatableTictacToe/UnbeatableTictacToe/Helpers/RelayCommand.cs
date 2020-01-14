using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace UnbeatableTictacToe.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _command;
        private readonly Func<object, bool> _canExecute;
        public event EventHandler CanExecuteChanged = delegate { };

        public RelayCommand(Action<object> command, Func<object, bool> canExecute = null)
        {
            _canExecute = canExecute;
            _command = command ?? throw new ArgumentNullException();
        }

        public void Execute(object parameter = null)
        {
            _command.Invoke(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

    }
}
