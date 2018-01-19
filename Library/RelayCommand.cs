using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library
{
    public class RelayCommand : ICommand
    {
        #region Private members

        private readonly Action<object> mExecute;
        private readonly Predicate<object> mCanExecute;
        private readonly List<string> mDependentPropertyNames;

        #endregion // Private members

        #region Constructors
        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execute">Delegate for execute command</param>
        /// <param name="canExecute">Expression tree for the canExecute command</param>
        public RelayCommand(Action<object> execute, Expression<Predicate<object>> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("RelayCommand - execute");
            mExecute = execute;

            mDependentPropertyNames = new List<string>();
            if (canExecute != null)
            {
                mCanExecute = canExecute.Compile();

                var expression = canExecute.Body as MemberExpression;
                if (expression != null)
                {
                    var cex = expression.Expression as ConstantExpression;
                    if (cex != null && cex.Value != null)
                    {
                        INotifyPropertyChanged target = cex.Value as INotifyPropertyChanged;
                        target.PropertyChanged += TargetPropertyChanged;
                        mDependentPropertyNames.Add(expression.Member.Name);
                    }
                }
            }
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return mCanExecute == null ? true : mCanExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object aParameter)
        {
            if (CanExecute(aParameter))
            {
                mExecute(aParameter);
            }
        }

        /// <summary>
        /// RaiseCanExecuteChanged for for properties that are monitored
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Property</param>
        private void TargetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (mDependentPropertyNames.Contains(e.PropertyName))
            {
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Refresh for commands that are active (active window)
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion // ICommand Members
    }
}

