using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodCalculator.Commands
{
    public class ButtonCommand : CommandBase
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ButtonCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? null!;
        }
        public bool CanExecute(object? parameter)
        {
            return this.canExecute == null || this.canExecute(parameter ?? null!);
        }

        public override void Execute(object? parameter)
        {
            this.execute(parameter ?? null!);
        }        
    }
}
