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
        private ObservableCollection<Customer> _customerList;
        public ObservableCollection<Customer> CustomerList
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
        private static Customer _seletectedCustomer;
        public static Customer SelectedCustomer
        {
            get => _seletectedCustomer;
            set => _seletectedCustomer = value;
        }
        
        public RelayCommand AddCustCommand { get; set; }
        public RelayCommand EditCustCommand { get; set; }
        public RelayCommand DeleteCustCommand { get; set; }

        public CustomerDb cdb { get; set; }

        #endregion

        #region Methods
        private void startUpInitializer()
        {
            cdb = new CustomerDb();
            EventReferencerCustomer.AddReference("custEntry", cdb);
            CustomerList = new ObservableCollection<Customer>(cdb.GetCustomerList());

            AddCustCommand = new RelayCommand(addCustBtnClicked, canClickAddCutBtn);
            EditCustCommand = new RelayCommand(editCustBtnClicked, canClickEditCustBtn);
            DeleteCustCommand = new RelayCommand(deleteCustBtnClicked, canClickDeleteCustBtn);

            cdb.CustomerInsertEvent += updateCustomerList;
        }

        private void addCustBtnClicked()
        {
            View.AddCustomerView cview = new View.AddCustomerView();
            cview.Owner = Application.Current.MainWindow;
            cview.ShowDialog();
        }
        private bool canClickAddCutBtn()
        {
            return true;
        }

        private void editCustBtnClicked()
        {

        }

        private bool canClickEditCustBtn()
        {
            return false;
        }

        private void deleteCustBtnClicked()
        {

        }
        private bool canClickDeleteCustBtn()
        {
            return false;
        }

        private void updateCustomerList(Object sender, EventArgs e)
        {
            CustomerIndex = -1;
            CustomerDb db = EventReferencerCustomer.GetReference("custEntry");
            Customer c = db.GetLastInsertedCustomer();
            if (c.Id != "0")
            {
                CustomerList.Add(c);
            }
        }
        #endregion
    }
}
