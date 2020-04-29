using System;

namespace ClientMessenger.Commands
{
    class BaseButtonCommand : BaseCommand
    {
        public BaseButtonCommand(Action<object> execute, Func<object, bool> canExecute = null) : base(execute, canExecute)
        {
        }

        public new void Execute(object parameter)
        {
            base.Execute(parameter);
        }
    }
}
