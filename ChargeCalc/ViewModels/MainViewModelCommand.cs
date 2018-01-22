using Ingres.Client;
using Library;
using SQLRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace ChargeCalc.ViewModels
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

        public void Connect()
        {
            if (SelectedServer != null)
            {                
                IngresConnection i_con = new IngresConnection(SelectedDatabase.ConnectString);
                i_con.Open();
                i_con.Close();
               
            }
        }

        #endregion // Command Properties
    }
}
