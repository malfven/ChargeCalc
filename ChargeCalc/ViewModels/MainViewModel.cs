using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChargeCalc.ViewModels
{
    
    public partial class MainViewModel : BaseViewModel
    {
        private string mTest;
        
        public MainViewModel()
        {
            Test = "kaka2";
            SetupCommands();
        }

        public string Test { get => mTest; set => mTest = value; }



    }


}
