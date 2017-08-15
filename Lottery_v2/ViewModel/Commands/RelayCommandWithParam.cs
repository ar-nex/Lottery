using System;
using System.Windows.Input;

namespace Lottery_v2.ViewModel.Commands
{
    public class RelayCommandWithParam : ICommand
    {
        private Action<Object> ExecuteMethod;
        private Func<bool> CanExecuteMethod;
        private event EventHandler CanExecuteChangedInternal;


        public RelayCommandWithParam(Action<Object> ex, Func<bool> canexe)
        {
            this.ExecuteMethod = ex;
            this.CanExecuteMethod = canexe;
        }


        public bool CanExecute(object parameter)
        {
            return CanExecuteMethod();
        }

       

        public void Execute(object parameter)
        {
            ExecuteMethod(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }
    }
}
