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
                string sql = "select " +
                  "trim(a.table_name) as name, trim(a.table_owner) as owner, a.table_type as typ, a.num_rows as rows, " +
                  "ifnull(b.file_name, '') +'.'+ ifnull(b.file_ext,'') as filename " +
                 "from iitables a left join iifile_info b " +
                 "on a.table_name = b.table_name " +
                 "and a.table_owner = b.owner_name " +
                 "where a.table_name not like 'ii%' and  " +
                 "( a.table_type = 'T' or a.table_type = 'V') order by name";

                IngresConnection i_con = new IngresConnection(SelectedDatabase.ConnectString);
                
                //IngresDataAdapter i_da = new IngresDataAdapter(sql, i_con);
                //i_da.Fill(m_tables);
                i_con.Open();
                i_con.Close();

                               
                //IngresConnection i_con = new IngresConnection(frmMain.SelectedDatabase.ConnectString);
                //IngresDataAdapter i_da = new IngresDataAdapter(sql, i_con);
                //i_da.Fill(m_tables);
                //i_con.Close();
                //m_bstab.DataSource = m_tables;
                //dgvTable.DataSource = m_bstab;

            }
        }

        #endregion // Command Properties
    }
}
