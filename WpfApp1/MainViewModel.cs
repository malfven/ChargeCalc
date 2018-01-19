using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    
    public class MainViewModel
    {
        private string mTest;

        public MainViewModel()
        {
            Test = "kaka";
        }

        public string Test { get => mTest; set => mTest = value; }

       
    }
}
