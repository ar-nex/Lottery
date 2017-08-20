using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Lottery_v2.Model.Database
{
    public class SaleDb : BaseDb
    {
        public SaleDb()
            : base()
        {

        }

        // this hold the customer id for the last transaction and will be used to update customer currrent due in customerveiw model
        private string lastCustomerId;
        public event EventHandler SaleInsertEvent;
        public void OnSaleInsertEvent()
        {
            SaleInsertEvent?.Invoke(this, EventArgs.Empty);
        }

        public int InsertSaleInfo(Sale s)
        {
            int insertedSaleId = 0;
            string sql1 = "INSERT INTO sale (customer_id) VALUES ('"+s.CustId+"')";
            string sql2 = "INSERT INTO current_due (amount, customer_id) VALUES ('"+s.CustCurrDue+"', '"+s.CustId+"')";
            
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                MySqlTransaction myTrans = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = myTrans;
                try
                {
                    // insert into sale table and capture the sale id
                    cmd.CommandText = sql1;
                    cmd.ExecuteNonQuery();
                    insertedSaleId = (int)cmd.LastInsertedId;

                    // insert into customer due table
                    cmd.CommandText = sql2;
                    cmd.ExecuteNonQuery();

                    // insert into sold item table with the sale id
                    string sql3 = "INSERT INTO sold_item (product_name, product_type, quantity, rate, sale_id, sale_customer_id) VALUES";
                    foreach (SoldItem item in s.SoldItems)
                    {
                        sql3 = sql3 + "('" + item.ProductName + "', '" + item.ProductType + "', '" + item.Quantity + "', '" + item.Rate + "', '" + insertedSaleId + "', '" + s.CustId + "'), ";
                    }
                    int lastIndexofComma = sql3.LastIndexOf(',');
                    sql3 = sql3.Remove(lastIndexofComma, 2);
                    cmd.CommandText = sql3;
                    cmd.ExecuteNonQuery();
                    
                    // insert into payment id with sale id
                    string sql4 = "INSERT INTO payment (cash, pt, bonus, sale_id, customer_id) VALUES ('" + s.CashPayment + "', '" + s.PtPayment + "', '" + s.BonusPayment + "', '" + insertedSaleId + "', '" + s.CustId + "')";
                    cmd.CommandText = sql4;
                    cmd.ExecuteNonQuery();

                    // if there is pt payment insert into pt detail table
                    if (s.PtPayment > 0)
                    {
                        string sql5 = "INSERT INTO pt_details (amount, type) VALUES ('" + s.PtPayment + "', 'PAYMENT')";
                        cmd.CommandText = sql5;
                        cmd.ExecuteNonQuery();
                    }
                    myTrans.Commit();
                    lastCustomerId = s.CustId;
                    // raise the event
                    OnSaleInsertEvent();
                }
                catch (Exception st)
                {
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch (Exception sr)
                    {
                        System.Windows.MessageBox.Show("DB sale trans Rollback failed. : "+sr.Message);
                    }
                    System.Windows.MessageBox.Show("DB Sale transaction failed. : "+st.Message);
                    lastCustomerId = string.Empty;
                }
            }
            catch (Exception se2)
            {
                System.Windows.MessageBox.Show("Sale DB Problem. : "+se2.Message);
            }
            finally
            {
                conn.Close();
            }
            
            return insertedSaleId;
        }

        public CustDueDetails GetLastCustomerCurrentDue()
        {
            CustDueDetails dueDt = new CustDueDetails();
            if (string.IsNullOrEmpty(lastCustomerId))
            {
                return dueDt;
            }
            else
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM current_due WHERE customer_id = '"+lastCustomerId+"'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        CustDueDetails duedetails = new CustDueDetails(rdr[0].ToString(), (decimal)rdr[1], (DateTime)rdr[2]);
                        dueDt = duedetails;
                    }
                }
                catch (Exception lsdue)
                {
                    System.Windows.MessageBox.Show("Last Entry Due : "+lsdue.Message);
                }
                finally
                {
                    conn.Close();
                    
                }
                return dueDt;
            }
           
        }
    }
}
