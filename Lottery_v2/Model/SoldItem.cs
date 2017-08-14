using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lottery_v2.Model
{
    public class SoldItem
    {
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }

        public SoldItem()
        {

        }

        public SoldItem(string pname, string ptype, int quant, decimal rt)
        {
            ProductName = pname;
            ProductType = ptype;
            Quantity = quant;
            Rate = rt;
        }
    }
}
