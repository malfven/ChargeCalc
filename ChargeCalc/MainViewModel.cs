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
        private string mTest;
        
        public MainViewModel()
        {
            Test = "kaka2";
            SetupCommands();
        }

        public string Test { get => mTest; set => mTest = value; }

       
    }
}
