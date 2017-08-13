using System;
using System.Collections.ObjectModel;
using System.Linq;
using Lottery_v2.Model;
using Lottery_v2.Model.Database;
using Lottery_v2.ViewModel.Commands;
using System.Windows;

namespace Lottery_v2.ViewModel
{
    public class MainWindowCustomerViewModel : BaseViewModel
    {
        public MainWindowCustomerViewModel()
            : base()
        {
            startUpInitializer();
        }

        #region property
        private ObservableCollection<Product> _customerList;
        public ObservableCollection<Product> CustomerList
        {
            get => _customerList;
            set
            {
                _customerList = value;
                OnPropertyChanged("CustomerList");
            }
        }

        private int _customerIndex;
        public int CustomerIndex
        {
            get => _customerIndex;
            set
            {
                if (value < -0 || value > CustomerList.Count())
                {
                    _customerIndex = -1;
                    SelectedCustomer = null;
                }
                else
                {
                    _customerIndex = value;
                    SelectedCustomer = CustomerList[value];
                }
                OnPropertyChanged("CustomerIndex");
            }
        }

        /// <summary>
        /// Used to send selected product to the edit window
        /// </summary>
        private static Product _seletectedCustomer;
        public static Product SelectedCustomer
        {
            get => _seletectedCustomer;
            set => _seletectedCustomer = value;
        }


        #endregion

        #region Methods
        private void startUpInitializer()
        {

        }
        #endregion
    }
}
