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
    /// A Command implementation for the MVVM pattern using events
    /// (inspired by Josh Smith work about RelayCommand and MVVM).
    /// </summary>
    public class SignalCommand : ICommand
    {
        private event EventHandler<EventArgs<object>> _executed;

        /// <summary>
        /// Event fired when the execution occurs.
        /// </summary>
        public event EventHandler<EventArgs<object>> Executed
        {
            add
            {
                _executed += value;
            }
            remove
            {
                _executed -= value;
            }
        }

        /// <summary>
        /// Fires the Executed event.
        /// </summary>
        protected void OnExecuted(object parameter)
        {
            if (_executed != null)
            {
                EventArgs<object> e = new EventArgs<object>(parameter);
                _executed(this, e);
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
        /// Fires the CanExecuteChecking event. If not assigned, a value of true is returned.
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

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return OnCanExecuteChecking(parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
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

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            OnExecuted(parameter);
        }

        #endregion
    }
}
