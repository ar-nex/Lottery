using System;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Lottery_v2.Model.Database
{
    public class BaseDb
    {
        protected string connStr;
        protected MySqlConnection conn;

        public BaseDb()
        {
            if (ConfigurationManager.ConnectionStrings["db_connection"] != null)
            {
                try
                {
                    this.connStr = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
                    this.conn = new MySqlConnection();
                    this.conn.ConnectionString = this.connStr;
                }
                catch (Exception e_constr)
                {
                    System.Windows.MessageBox.Show("e_constr : " + e_constr.Message);
                }
            }
            else
            {
                this.connStr = "server=127.0.0.1;uid=delphinium_admin;" + "pwd=dark.d@tura;database=sathi_lottery;Convert Zero Datetime=True";
                this.conn = new MySqlConnection();
                this.conn.ConnectionString = this.connStr;
            }
        }
    }
}
