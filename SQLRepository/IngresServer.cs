using Ingres.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SQLRepository
{
    public class IngresServer
    {
        private IngresDatabase[] m_databaser = null;
        private string m_descr;
        private string m_name;
        private string m_passw;
        private string m_user;

        /// <summary>
        ///
        /// </summary>
        /// <param name="serversetup"></param>
        public IngresServer(XmlNode serversetup)
        {
            m_name = GetXMLText(serversetup, "Name");
            m_descr = GetXMLText(serversetup, "Descr");
            m_user = GetXMLText(serversetup, "User");
            m_passw = GetXMLText(serversetup, "Password");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbname"></param>
        /// <returns></returns>
        public string ConnectString(string dbname)
        {
            return string.Format("Server={0};Port=II7;Database={1};User ID={2};Password={3};Blank Date=null", m_name, dbname, m_user, m_passw);
        }

        /// <summary>
        ///
        /// </summary>
        public void GetDataBaseNames()
        {
            try
            {
                //        IngresConnection i_con = new IngresConnection("Server=(local);Port=II7;Database=iidbdb;User ID=christer;Password=hemma;Blank Date=null");
                IngresConnection i_con = new IngresConnection(ConnectString("iidbdb"));
                string sql = "select name, own from iidatabase where own <> '$ingres' order by name";
                IngresDataAdapter i_da = new IngresDataAdapter(sql, i_con);
                DataTable dt = new DataTable();
                i_da.Fill(dt);
                i_con.Close();
                m_databaser = new IngresDatabase[dt.Rows.Count];
                int i = 0;
                foreach (DataRow r in dt.Rows)
                    m_databaser[i++] = new IngresDatabase(this, r);
            }
            catch (IngresException fel)
            {
                throw new Exception("FEL", fel);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_descr;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="n"></param>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        private string GetXMLText(XmlNode n, string keyvalue)
        {
            string s;
            try
            {
                s = n.SelectSingleNode(keyvalue).InnerText;
            }
            catch (Exception)
            {
                s = "";
            }
            return s;
        }

        public IngresDatabase[] DataBases { get { return m_databaser; } }
    }
}
