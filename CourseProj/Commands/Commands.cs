using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProj.Commands
{
    public class Commands : ICommand
    {
        protected Action<object> _action;
        protected Predicate<object> _predicate;

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            if (_predicate == null)
                return true;

            return _predicate(parameter);
        }

        public virtual void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
