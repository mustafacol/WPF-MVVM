using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MustafaCol.Commands
{
    class CrudCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action action;

        public CrudCommand(Action work)
        {
            action = work;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter) { 
            action();
        }
    }
}
