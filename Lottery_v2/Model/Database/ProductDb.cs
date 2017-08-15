using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Lottery_v2.Model.Database
{
    public class ProductDb : BaseDb
    {
        public ProductDb()
        : base()
        {

        }

        public event EventHandler ProductInsertEvent;

        public void OnProductInsertEvent()
        {
            ProductInsertEvent?.Invoke(this, EventArgs.Empty);
        }

        // public int _lastProductId;
        private int _lastProductId;

        public Product GetLastInsertedProduct()
        {
            Product p = new Product();
            if (_lastProductId > 0)
            {
                try
                {
                    this.conn.Open();
                    string sql = "SELECT id, name, type, rate, last_updated FROM product WHERE id ="+_lastProductId;
                    MySqlCommand cmd = new MySqlCommand(sql, this.conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        p.Id = rdr[0].ToString();
                        p.Name = rdr[1].ToString();
                        p.Type = rdr[2].ToString();
                        p.Rate = Convert.ToDecimal(rdr[3]);
                        p.LastUpdated = DateTime.Parse(rdr[4].ToString());
                        // reset lastinserted id;
                       // _lastProductId = 0;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    this.conn.Close();
                }
            }
            else
            {
                p.Id = "0";
            }

            return p;
        }

        public List<Product> GetProductList()
        {
            List<Product> plist = new List<Product>();
            try
            {
                this.conn.Open();
                string sql = "SELECT id, name, type, rate, last_updated FROM product WHERE 1";
                MySqlCommand cmd = new MySqlCommand(sql, this.conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product p = new Product();
                    p.Id = rdr[0].ToString();
                    p.Name = rdr[1].ToString();
                    p.Type = rdr[2].ToString();
                    p.Rate = Convert.ToDecimal(rdr[3]);
                    p.LastUpdated = DateTime.Parse(rdr[4].ToString());

                    plist.Add(p);
                }
            }
            catch (Exception ep)
            {
                System.Windows.MessageBox.Show("e_db_p: "+ep.Message);
            }
            finally
            {
                this.conn.Close();
            }

            return plist;
        }


        public int InsertProduct(Product p)
        {
            int insertedId = 0;
            string name = "'" + p.Name + "'";
            string type = string.IsNullOrEmpty(p.Type) ? "NULL" : "'" + p.Type + "'";
            string rate = p.Rate > 0 ? p.Rate.ToString("#.##") : "0";
            try
            {
                this.conn.Open();
                string sql = "INSERT INTO PRODUCT (name, type, rate, last_updated) VALUES (" + name + ", " + type + ", " + rate + ", NOW())";
                MySqlCommand cmd = new MySqlCommand(sql, this.conn);
                cmd.ExecuteNonQuery();
                long last_inserted_id = cmd.LastInsertedId;
                insertedId = (int)last_inserted_id;
                _lastProductId = insertedId;     
            }
            catch (Exception ep1)
            {
                System.Windows.MessageBox.Show("e_db_p1: " + ep1.Message);
            }
            finally
            {
                this.conn.Close();
            }
            if (insertedId > 0)
            {
                OnProductInsertEvent();
            }
            return insertedId;
        }

        public int UpdateProduct(Product p)
        {
            int affectedRows = 0;
            string name = "'" + p.Name + "'";
            string type = string.IsNullOrEmpty(p.Type) ? "NULL" : "'" + p.Type + "'";
            string rate = p.Rate > 0 ? p.Rate.ToString("#.##") : "0";
            try
            {
                this.conn.Open();
                string sql = "UPDATE product SET name=" + name + ", type=" + type + ", rate=" + rate + ", last_updated=NOW() WHERE id=" + p.Id;
                MySqlCommand cmd = new MySqlCommand(sql, this.conn);
                affectedRows = cmd.ExecuteNonQuery();       
            }
            catch (Exception ep2)
            {
                System.Windows.MessageBox.Show("e_db_p2: " + ep2.Message);
            }
            finally
            {
                this.conn.Close();
            }
            return affectedRows;
        }

        public int DeleteProduct(string product_id)
        {
            int affectedRows = 0;
            try
            {
                this.conn.Open();
                string sql = "DELETE FROM product WHERE id="+product_id;
                MySqlCommand cmd = new MySqlCommand(sql, this.conn);
                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ep3)
            {
                System.Windows.MessageBox.Show("e_db_p3: " + ep3.Message);
            }
            finally
            {
                this.conn.Close();
            }
            return affectedRows;
        }
    }
}
