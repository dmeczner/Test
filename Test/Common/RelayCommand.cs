using System;
using System.Windows.Input;

namespace Test.Common
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute { get; set; }
        private Predicate<object> canExecute { get; set; }
        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
