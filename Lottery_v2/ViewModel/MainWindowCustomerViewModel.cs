﻿using System;
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

        private Customer _selecSaleCustomer;
        public Customer SelecSaleCustomer
        {
            get => _selecSaleCustomer;
            set
            {
                _selecSaleCustomer = value;
                OnPropertyChanged("SelecSaleCustomer");
                updateSaleCustDetail();
            }
        }

        private string _saleCustId;
        public string SaleCustId
        {
            get => _saleCustId;
            set
            {
                _saleCustId = value;
                OnPropertyChanged("SaleCustId");
            }
        }

        private string _saleCustAgency;
        public string SaleCustAgency
        {
            get => _saleCustAgency;
            set
            {
                _saleCustAgency = value;
                OnPropertyChanged("SaleCustAgency");
            }
        }

        private decimal _selecCustDue;
        public decimal SelecCustDue
        {
            get => _selecCustDue;
            set
            {
                _selecCustDue = value;
                OnPropertyChanged("SelecCustDue");
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
            CustomerIndex = -1;
            SelectedCustomer = new Customer();

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
            View.EditCustomerView ecv = new View.EditCustomerView();
            ecv.Owner = Application.Current.MainWindow;
            ecv.ShowDialog();
        }

        private bool canClickEditCustBtn()
        {
            return (CustomerIndex > -1 && CustomerIndex < CustomerList.Count);
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

        private void updateSaleCustDetail()
        {
            if (SelecSaleCustomer != null && CustomerList.IndexOf(SelecSaleCustomer) != -1)
            {
                SaleCustId = SelecSaleCustomer.Id;
                SaleCustAgency = SelecSaleCustomer.Agency;
                SelecCustDue = SelecSaleCustomer.PreviousDue;
            }
            else
            {
                SaleCustId = string.Empty;
                SaleCustAgency = string.Empty;
                SelecCustDue = 0;
            }
        }
        #endregion
    }
}
