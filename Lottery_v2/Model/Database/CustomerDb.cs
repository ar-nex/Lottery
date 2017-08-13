using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace Lottery_v2.Model.Database
{
    public class CustomerDb : BaseDb
    {
        public CustomerDb()
        :base()
        {

        }

        public List<Customer> GetCustomerList()
        {
            List<Customer> clist = new List<Customer>();
            string sql = "SELECT c.id, c.name, c.agency, c.address, c.mobile, c.joining_date, d.amount FROM customer c INNER JOIN current_due d ON d.customer_id = c.id";
            try
            {
                this.conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, this.conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customer c = new Customer();
                    c.Id = rdr[0].ToString();
                    c.Name = rdr[1].ToString();
                    c.Agency = rdr[2].ToString();
                    c.Address = rdr[3].ToString();
                    c.Mobile = rdr[4].ToString();
                    c.JoiningDate = (string.IsNullOrEmpty(rdr[5].ToString())) ? default(DateTime) : DateTime.Parse(rdr[5].ToString());
                    c.PreviousDue = Convert.ToDecimal(rdr[6]);

                    clist.Add(c);
                }

            }
            catch (Exception e1)
            {
                System.Windows.MessageBox.Show("Model|Database|CustomerDb|GetCustomerList: "+e1.Message);
            }
            finally
            {
                this.conn.Close();
            }
            return clist;
        }


        public int AddCustomer(Customer c)
        {
            int insertedId = 0;
            string name = "'"+c.Name+"'";
            string agency = (string.IsNullOrEmpty(c.Agency)) ? "NULL" : "'" + c.Agency + "'";
            string address = (string.IsNullOrEmpty(c.Address)) ? "NULL" : "'" + c.Address + "'";
            string mobile = (string.IsNullOrEmpty(c.Mobile)) ? "NULL" : "'" + c.Mobile + "'";
            string joiningDate = (DateTime.Compare(c.JoiningDate, default(DateTime)) == 0) ? "NULL" : c.JoiningDate.ToString("yyyy-MM-dd") ;
            string due = (c.PreviousDue > 0) ? c.PreviousDue.ToString("#.##") : "0";

            try
            {
                this.conn.Open();
                MySqlCommand cmd = this.conn.CreateCommand();
                MySqlTransaction myTrans;
                myTrans = this.conn.BeginTransaction();
                cmd.Connection = this.conn;
                cmd.Transaction = myTrans;
                try
                {
                    string sql_customer = "INSERT INTO customer(name, agency, address, mobile, joining_date) VALUES ("+name+", "+agency+", "+address+", "+mobile+", "+joiningDate+")";
                    cmd.CommandText = sql_customer;
                    cmd.ExecuteNonQuery();
                    long entry_id = cmd.LastInsertedId;
                    insertedId = (int)entry_id;

                    string sql_due = "INSERT INTO current_due(amount, customer_id) VALUES (" + due + ", " + insertedId + ")";
                    cmd.CommandText = sql_due;
                    cmd.ExecuteNonQuery();

                    myTrans.Commit();
                }
                catch (Exception e21)
                {
                    try
                    {
                        myTrans.Rollback();
                        insertedId = 0;
                    }
                    catch (Exception e221)
                    {
                        System.Windows.MessageBox.Show("Model|Database|CustomerDb|AddCustomer: " + e221.Message);
                    }
                    System.Windows.MessageBox.Show("Model|Database|CustomerDb|AddCustomer: " + e21.Message);
                    insertedId = 0;
                }
            }
            catch (Exception e2)
            {
                System.Windows.MessageBox.Show("Model|Database|CustomerDb|AddCustomer: " + e2.Message);

            }
            finally
            {
                this.conn.Close();
            }

            return insertedId;
        
        }

        public int UpdateCustomer(Customer c)
        {
            int affectedRows = 0;
            string name = "'" + c.Name + "'";
            string agency = (string.IsNullOrEmpty(c.Agency)) ? "NULL" : "'" + c.Agency + "'";
            string address = (string.IsNullOrEmpty(c.Address)) ? "NULL" : "'" + c.Address + "'";
            string mobile = (string.IsNullOrEmpty(c.Mobile)) ? "NULL" : "'" + c.Mobile + "'";
            string joiningDate = (DateTime.Compare(c.JoiningDate, default(DateTime)) == 0) ? "NULL" : c.JoiningDate.ToString("yyyy-MM-dd");
            
            try
            {
                this.conn.Open();
                string sql = "UPDATE customer set name="+name+", agency="+agency+", address="+address+", mobile="+mobile+", joining_date = "+joiningDate+" WHERE id="+c.Id;
                MySqlCommand cmd = new MySqlCommand(sql,this.conn);
                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception e3)
            {
                System.Windows.MessageBox.Show("Model|Database|CustomerDb|UpdateCustomer: " + e3.Message);
            }
            finally
            {
                this.conn.Close();
            }
            return affectedRows;
        }

        public int DeleteCustomer(string id)
        {
            int affectedRows = 0;
            try
            {
                this.conn.Close();
                string sql = "";
            }
            catch (Exception e4)
            {
                System.Windows.MessageBox.Show("Model|Database|CustomerDb|DeleteCustomer: " + e4.Message);
            }
            finally
            {
                this.conn.Close();
            }
            return affectedRows;
        }
    }
}
