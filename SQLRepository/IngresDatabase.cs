using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace SQLRepository
{
    public class IngresDatabase
    {
        private string m_name;
        private string m_owner;
        private IngresServer m_server;

        /// <summary>
        ///
        /// </summary>
        /// <param name="srv"></param>
        /// <param name="r"></param>
        public IngresDatabase(IngresServer srv, DataRow r)
        {
            m_server = srv;
            m_name = ((string)r["name"]).Trim();
            m_owner = ((string)r["own"]).Trim();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_name;
        }

        public string ConnectString { get { return m_server.ConnectString(m_name); } }
        public string DBName { get { return m_name; } set { m_name = value; } }
        public string DBOwner { get { return m_owner; } set { m_owner = value; } }
    }

}
