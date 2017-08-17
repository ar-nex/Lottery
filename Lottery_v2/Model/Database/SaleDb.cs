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

        public event EventHandler SaleInsertEvent;
        public void OnSaleInsertEvent()
        {
            SaleInsertEvent?.Invoke(this, EventArgs.Empty);
        }



    }
}
