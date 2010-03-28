//------------------------------------------------------------------
//
//  For licensing information and to get the latest version go to:
//  http://www.codeplex.com/perspective
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//  FITNESS FOR A PARTICULAR PURPOSE.
//
//------------------------------------------------------------------
using System;
using System.Windows.Input;

namespace Perspective.Core.Wpf.Data
{
    /// <summary>
    /// A Command implementation for the MVVM pattern using events.
    /// </summary>
    public class SignalCommand : ICommand
    {
        private event EventHandler<EventArgs<object>> _executing;

        /// <summary>
        /// Event fired when the execution occurs.
        /// </summary>
        public event EventHandler<EventArgs<object>> Executing
        {
            add
            {
                _executing += value;
            }
            remove
            {
                _executing -= value;
            }
        }

        /// <summary>
        /// Fires the Executing event.
        /// </summary>
        protected void OnExecuting(object parameter)
        {
            if (_executing != null)
            {
                EventArgs<object> e = new EventArgs<object>(parameter);
                _executing(this, e);
            }
        }

        private event EventHandler<CancelEventArgs<object>> _canExecuteChecking;

        /// <summary>
        /// Event fired when the CanExecute checking occurs.
        /// </summary>
        public event EventHandler<CancelEventArgs<object>> CanExecuteChecking
        {
            add
            {
                _canExecuteChecking += value;
            }
            remove
            {
                _canExecuteChecking -= value;
            }
        }

        /// <summary>
        /// Fires the Executing event. If not assigned, a value of true is returned.
        /// </summary>
        protected bool OnCanExecuteChecking(object parameter)
        {
            bool canExecute = true;
            if (_canExecuteChecking != null)
            {
                CancelEventArgs<object> e = new CancelEventArgs<object>(parameter);
                _canExecuteChecking(this, e);
                canExecute = !e.Cancel;
            }
            return canExecute;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return OnCanExecuteChecking(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            OnExecuting(parameter);
        }

        #endregion
    }
}
