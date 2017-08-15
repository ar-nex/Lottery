using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lottery_v2.Model
{
    public class SoldItem : ViewModel.BaseViewModel
    {
        public SoldItem()
            : base()
        {

        }

        public string ProductName { get; set; }
        public string ProductType { get; set; }
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                {
                    _quantity = 0;
                }
                else
                {
                    _quantity = value;
                }
                OnPropertyChanged("Quantity");
            }
        }
        public decimal Rate { get; set; }
        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }
    }
}
