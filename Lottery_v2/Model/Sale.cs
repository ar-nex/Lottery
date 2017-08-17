using System;
using System.Collections.Generic;

namespace Lottery_v2.Model
{
    public class Sale : ViewModel.BaseViewModel
    {
        public Sale()
            : base()
        {

        }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private DateTime _sellingTime;
        public DateTime SellingTime
        {
            get => _sellingTime;
            set
            {
                _sellingTime = value;
                OnPropertyChanged("SellingTime");
            }
        }

        private string _custId;
        public string CustId
        {
            get => _custId;
            set
            {
                _custId = value;
                OnPropertyChanged("CustId");
            }
        }

        private List<SoldItem> _soldItems;
        public List<SoldItem> SoldItems
        {
            get => _soldItems;
            set
            {
                _soldItems = value;
                OnPropertyChanged("SoldItems");
            }
        }

        private decimal _custCurrDue;
        public decimal CustCurrDue
        {
            get => _custCurrDue;
            set
            {
                _custCurrDue = value;
                OnPropertyChanged("CustCurrDue");
            }
        }

        private decimal _cashPayment;
        public decimal CashPayment
        {
            get => _cashPayment;
            set
            {
                _cashPayment = value;
                OnPropertyChanged("CashPayment");
            }
        }

        private decimal _ptPayment;
        public decimal PtPayment
        {
            get => _ptPayment;
            set
            {
                _ptPayment = value;
                OnPropertyChanged("PtPayment");
            }
        }

        private decimal _bonusPayment;
        public decimal BonusPayment
        {
            get => _bonusPayment;
            set
            {
                _bonusPayment = value;
                OnPropertyChanged("BonusPayment");
            }
        }
    }
}
