﻿using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class MainViewModel
    {
        public void SetupCommands()
        {
            ConnectCommand = new RelayCommand(aParam =>
                Connect(),
                aP => CanConnect);
        }

        #region Command Properties
        public ICommand ConnectCommand
        {
            get;
            private set;
        }

        public bool CanConnect
        {
            get
            {
                return
                   true;
            }
        }

        private void Connect()
        {
            int kaka;
            kaka = 1 + 2;
        }

        #endregion // Command Properties
    }
}
