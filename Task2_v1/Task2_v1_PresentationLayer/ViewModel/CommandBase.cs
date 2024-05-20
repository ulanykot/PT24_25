using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    internal class CommandBase : ICommand
    {
        private readonly Action m_Execute;
        private readonly Func<bool> m_CanExecute;

        public event EventHandler CanExecuteChanged;
        public CommandBase(Action execute) : this(execute, null) { }
        public CommandBase(Action execute, Func<bool> canExecute)
        {
            m_Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            m_CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (m_CanExecute == null)
                return true;
            if (parameter == null)
                return m_CanExecute();
            return m_CanExecute();
        }

        public void Execute(object parameter)
        {
            m_Execute();
        }
        internal void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
