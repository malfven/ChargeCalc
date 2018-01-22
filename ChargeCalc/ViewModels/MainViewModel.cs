using Microsoft.Win32;
using SQLRepository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace ChargeCalc.ViewModels
{

    public partial class MainViewModel : BaseViewModel
    {
        #region Members
        private string mTest;
               
        private ObservableCollection<IngresServer> m_ServerList;
        private IngresServer m_SelectedServer;
        private IngresDatabase m_SelectedDatabase;

        //private IngresServer[] m_srvlist = null;
              
        #endregion

        #region Constructors

        public MainViewModel()
        {
            Test = "kaka2";
            LoadSetup(@"Software\Hoganas\IngresSQL");
                        
            SetupCommands();
        }
        #endregion

        #region Public properties
        public IngresServer SelectedServer
        {
            get
            {
                return m_SelectedServer;
            }
            set
            {
                m_SelectedServer = value;
                m_SelectedServer.GetDataBaseNames();

                NotifyPropertyChanged("SelectedServer");
            }
        }

        public IngresDatabase SelectedDatabase
        {
            get
            {
                return m_SelectedDatabase;
            }
            set
            {
                m_SelectedDatabase = value;
                NotifyPropertyChanged("SelectedDatabase");
            }
        }
        public string Test { get => mTest; set => mTest = value; }
        public ObservableCollection<IngresServer> ServerList { get => m_ServerList; set => m_ServerList = value; }
        #endregion

        #region public methods
        /// <summary>
        ///
        /// </summary>
        /// <param name="regkey"></param>
        public void LoadSetup(string regkey)
        {
            RegistryKey regbase = Registry.LocalMachine.OpenSubKey(regkey);
            string filename = "";
            if (regbase != null)
            {
                filename = (string)regbase.GetValue("SetupFile");
                regbase.Close();
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(filename);
                XmlNode root = xdoc.SelectSingleNode("IngresSQL");
                XmlNodeList xnl = root.SelectSingleNode("NodeList").SelectNodes("Server");
               
                // Load list of servers
                m_ServerList = new ObservableCollection<IngresServer>();                
                foreach (XmlNode n in xnl)
                    m_ServerList.Add(new IngresServer(n));                    
            }
            else
            {
                throw new Exception(string.Format("Setupfile name is missing in registry (\"{0} {1}\")",
                  regkey, "SetupFile"));
            }
            #endregion
        }
    }

}
