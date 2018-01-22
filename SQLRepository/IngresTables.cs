using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Ingres.Client;

namespace SQLRepository
{
    public class IngresTables
    {
        //private BindingSource m_bscol;
        private BindingSource m_bstab;
        //private DataTable m_columns;
        private DataTable m_tables;

        public DataTable Tables { get => m_tables; set => m_tables = value; }

        public IngresTables()
        {            
            m_bstab = new BindingSource();
            //m_bscol = new BindingSource();
            m_tables = new DataTable();
            m_tables.Columns.Add("name", typeof(string));
            //m_tables.Columns["name"].ColumnName = "Tabellnamn";
            m_tables.Columns.Add("owner", typeof(string));
            m_tables.Columns.Add("typ", typeof(string));
            m_tables.Columns.Add("rows", typeof(int));
            m_tables.Columns.Add("filename", typeof(string));
            m_tables.Columns.Add("alias", typeof(string));

            //m_columns = new DataTable();
            //m_columns.Columns.Add("name", typeof(string));
            //m_columns.Columns.Add("owner", typeof(string));
            //m_columns.Columns.Add("colname", typeof(string));
            //m_columns.Columns.Add("dtype", typeof(string));
            //m_columns.Columns.Add("dsize", typeof(int));
            //m_columns.Columns.Add("seq", typeof(int));
            //m_columns.Columns.Add("key", typeof(int));

            //DataGridViewColumn dgvc;
            //dgvTable.AutoGenerateColumns = false;

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "name";
            //dgvc.HeaderText = "Tabellnamn";
            //dgvc.Width = 90;
            //dgvc.Visible = true;
            //dgvTable.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "typ";
            //dgvc.HeaderText = "Typ";
            //dgvc.Width = 30;
            //dgvc.Visible = true;
            //dgvTable.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "owner";
            //dgvc.HeaderText = "Ägare";
            //dgvc.Width = 75;
            //dgvc.Visible = true;
            //dgvTable.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "rows";
            //dgvc.HeaderText = "Ant.poster";
            //dgvc.Width = 60;
            //dgvc.Visible = true;
            //dgvTable.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "filename";
            //dgvc.HeaderText = "Filnamn";
            //dgvc.Width = 100;
            //dgvc.Visible = true;
            //dgvTable.Columns.Add(dgvc);

            //dgvColumn.AutoGenerateColumns = false;
            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "name";
            //dgvc.HeaderText = "Tabellnamn";
            //dgvc.Width = 90;
            //dgvc.Visible = true;
            //dgvColumn.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "seq";
            //dgvc.HeaderText = "Pos";
            //dgvc.Width = 30;
            //dgvc.Visible = true;
            //dgvColumn.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "owner";
            //dgvc.HeaderText = "Ägare";
            //dgvc.Width = 75;
            //dgvc.Visible = false;
            //dgvColumn.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "colname";
            //dgvc.HeaderText = "Namn";
            //dgvc.Width = 75;
            //dgvc.Visible = true;
            //dgvColumn.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "dtype";
            //dgvc.HeaderText = "Typ";
            //dgvc.Width = 90;
            //dgvc.Visible = true;
            //dgvColumn.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "dsize";
            //dgvc.HeaderText = "Längd";
            //dgvc.Width = 40;
            //dgvc.Visible = true;
            //dgvColumn.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.DataPropertyName = "key";
            //dgvc.HeaderText = "Key";
            //dgvc.Width = 30;
            //dgvc.Visible = true;
            //dgvColumn.Columns.Add(dgvc);
                      
        }

        public void FillTable(string connectionString)
        {
            string sql = "select " +
                       "trim(a.table_name) as name, trim(a.table_owner) as owner, a.table_type as typ, a.num_rows as rows, " +
                       "ifnull(b.file_name, '') +'.'+ ifnull(b.file_ext,'') as filename " +
                      "from iitables a left join iifile_info b " +
                      "on a.table_name = b.table_name " +
                      "and a.table_owner = b.owner_name " +
                      "where a.table_name not like 'ii%' and  " +
                      "( a.table_type = 'T' or a.table_type = 'V') order by name";

            IngresConnection i_con = new IngresConnection(connectionString);
            IngresDataAdapter i_da = new IngresDataAdapter(sql, i_con);
            i_da.Fill(m_tables);
            i_con.Close();
            //m_bstab.DataSource = m_tables;
            //dgvTable.DataSource = m_bstab;
        }
    }
}
