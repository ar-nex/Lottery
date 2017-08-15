using System;
using System.Collections.ObjectModel;
using System.Linq;
using Lottery_v2.Model;
using Lottery_v2.Model.Database;
using Lottery_v2.ViewModel.Commands;
using System.Windows;

namespace Lottery_v2.ViewModel
{
    public class MainWindowSaleViewModel : BaseViewModel
    {
        public MainWindowSaleViewModel()
            : base()
        {
            StartUpInitializer();
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
                UpdateSaleCustDetail();
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

        private ObservableCollection<Product> _productList;
        public ObservableCollection<Product> ProductList
        {
            get => _productList;
            set
            {
                _productList = value;
                OnPropertyChanged("ProductList");
            }
        }

        private Product _selecProduct;
        public Product SelecProduct
        {
            get => _selecProduct;
            set
            {
                _selecProduct = value;
                OnPropertyChanged("SelecProduct");
                UpdateSelecProdDetails();
            }
        }

        private string _selecType;
        public string SelecType
        {
            get => _selecType;
            set
            {
                _selecType = value;
                OnPropertyChanged("SelecType");
            }
        }

        private decimal _selecRate;
        public decimal SelecRate
        {
            get => _selecRate;
            set
            {
                _selecRate = value;
                OnPropertyChanged("SelecRate");
            }
        }

        private int _selecProdQnt;
        public int SelecProdQnt
        {
            get => _selecProdQnt;
            set
            {
                if (value >= 0)
                {
                    _selecProdQnt = value;
                    SelecAmount = SelecRate * value;
                }
                else
                {
                    _selecProdQnt = 0;
                }
                OnPropertyChanged("SelecProdQnt");
            }
        }

        private decimal _selecAmount;
        public decimal SelecAmount
        {
            get => _selecAmount;
            set
            {
                _selecAmount = value;
                OnPropertyChanged("SelecAmount");
            }
        }


        private int _totalItems;
        public int TotalItems
        {
            get => _totalItems;
            set
            {
                _totalItems = (value >= 0) ? value : 0;
                OnPropertyChanged("TotalItems");
            }
        }

        private decimal _totalAmount;
        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = (value >= 0) ? value : 0;
                OnPropertyChanged("TotalAmount");
            }
        }



        private ObservableCollection<SoldItem> _soldItemList;
        public ObservableCollection<SoldItem> SoldItemList
        {
            get => _soldItemList;
            set
            {
                _soldItemList = value;
                OnPropertyChanged("SoldItemList");
            }
        }


        private decimal _totalAmountWithDue;
        public decimal TotalAmountWithDue
        {
            get => _totalAmountWithDue;
            set
            {
                _totalAmountWithDue = value;
                OnPropertyChanged("TotalAmountWithDue");
            }
        }

        private decimal _cashPayment;
        public decimal CashPayment
        {
            get => _cashPayment;
            set
            {
                _cashPayment = (value >= 0) ? value : 0;
                OnPropertyChanged("CashPayment");

            }
        }

        private decimal _ptPayment;
        public decimal PtPayment
        {
            get => _ptPayment;
            set
            {
                _ptPayment = (value >= 0) ? value : 0;
                OnPropertyChanged("PtPayment");
            }
        }

        private decimal _bonusPayment;
        public decimal BonusPayment
        {
            get => _bonusPayment;
            set
            {
                _bonusPayment = (value >= 0) ? value : 0;
                OnPropertyChanged("BonusPayment");
            }
        }

        private decimal _totalPayment;
        public decimal TotalPayment
        {
            get => _totalPayment;
            set
            {
                _totalPayment = (value >= 0) ? value : 0;
                OnPropertyChanged("TotalPayment");
            }
        }


        private string _finalBalanceLbl;
        public string FinalBalanceLbl
        {
            get => _finalBalanceLbl;
            set
            {
                _finalBalanceLbl = value;
                OnPropertyChanged("FinalBalanceLbl");
            }
        }

        private string _finalBalanceDetail;
        public string FinalBalanceDetail
        {
            get => _finalBalanceDetail;
            set
            {
                _finalBalanceDetail = value;
                OnPropertyChanged("FinalBalanceDetail");
            }
        }

        private decimal _finalBalanceAmount;
        public decimal FinalBalanceAmount
        {
            get => _finalBalanceAmount;
            set
            {
                _finalBalanceAmount = value;
                OnPropertyChanged("FinalBalanceAmount");
            }
        }

        private decimal _returnAmount;
        public decimal ReturnAmount
        {
            get => _returnAmount;
            set
            {
                _returnAmount = value;
                OnPropertyChanged("ReturnAmount");
            }
        }

        private decimal _remainingAfterReturn;
        public decimal RemainingAfterReturn
        {
            get => _remainingAfterReturn;
            set
            {
                _remainingAfterReturn = value;
                OnPropertyChanged("RemainingAfterReturn");
            }
        }

        private Visibility _returnVisibility;
        public Visibility ReturnVisibility
        {
            get => _returnVisibility;
            set
            {
                _returnVisibility = value;
                OnPropertyChanged("ReturnVisibility");
            }
        }

        public RelayCommand AddSoldItemCommand { get; set; }
        public RelayCommandWithParam RemoveSoldItemCommand { get; set; }

        public CustomerDb cdb { get; set; }
        public ProductDb pdb { get; set; }
        #endregion

        #region method
        private void StartUpInitializer()
        {
            SoldItemList = new ObservableCollection<SoldItem>();

            cdb = new CustomerDb();
            EventReferencerCustomer.AddReference("custEntry", cdb);
            cdb.CustomerInsertEvent += Cdb_CustomerInsertEvent;
            CustomerList = new ObservableCollection<Customer>(cdb.GetCustomerList());

            ProductDb pdb = new ProductDb();
            EventReferencer.AddReference("prodEntry", pdb);
            ProductList = new ObservableCollection<Product>(pdb.GetProductList());
            pdb.ProductInsertEvent += Pdb_ProductInsertEvent;

            FinalBalanceLbl = "FINAL BALANCE";
            FinalBalanceAmount = 0;
            FinalBalanceDetail = string.Empty;

            ReturnVisibility = Visibility.Hidden;

            AddSoldItemCommand = new RelayCommand(AddSoldItemBtnClicked, CanClickAddSoldItemBtn);
            RemoveSoldItemCommand = new RelayCommandWithParam(RemoveSoldItem, CanRemoveSoldItem);

        }

        private void Pdb_ProductInsertEvent(object sender, EventArgs e)
        {
            // ProductIndex = -1;
            ProductDb db = EventReferencer.GetReference("prodEntry");
            Product p = db.GetLastInsertedProduct();
            if (p.Id != "0")
            {
                ProductList.Add(p);
            }
        }

        private void Cdb_CustomerInsertEvent(object sender, EventArgs e)
        {
            // CustomerIndex = -1;
            CustomerDb db = EventReferencerCustomer.GetReference("custEntry");
            Customer c = db.GetLastInsertedCustomer();
            if (c.Id != "0")
            {
                CustomerList.Add(c);
            }
        }

        private void UpdateSaleCustDetail()
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

        private void UpdateSelecProdDetails()
        {
            if (SelecProduct != null && ProductList.IndexOf(SelecProduct) != -1)
            {
                SelecType = SelecProduct.Type;
                SelecRate = SelecProduct.Rate;
            }
            else
            {
                SelecType = string.Empty;
                SelecRate = 0;
            }
            SelecAmount = SelecRate * SelecProdQnt;
        }

        private void AddSoldItemBtnClicked()
        {
            SoldItem sitem = new SoldItem();
            sitem.ProductName = SelecProduct.Name;
            sitem.ProductType = SelecProduct.Type;
            sitem.Rate = SelecProduct.Rate;
            sitem.Quantity = SelecProdQnt;
            sitem.Amount = SelecAmount;
            // insert into collection
            SoldItemList.Add(sitem);
            // update total amount and items.
            UpdateTotalItemAmount();
            // Reset the box
            SelecProduct = new Product();
            SelecProdQnt = 0;
        }
        private bool CanClickAddSoldItemBtn()
        {
            bool validProdInfo = ((SelecProduct != null) && (ProductList.IndexOf(SelecProduct) != -1)) && (SelecProdQnt > 0);
            bool validCustInfo = (SelecSaleCustomer != null) && (CustomerList.IndexOf(SelecSaleCustomer) != -1);
            return validProdInfo && validCustInfo;
        }

        private void RemoveSoldItem(object parameter)
        {
            int index = SoldItemList.IndexOf(parameter as SoldItem);
            if (index > -1 && index < SoldItemList.Count)
            {
                SoldItemList.RemoveAt(index);
                UpdateTotalItemAmount();
            }
        }

        private bool CanRemoveSoldItem()
        {
            return true;
        }

        private void UpdateTotalItemAmount()
        {
            TotalItems = SoldItemList.Sum(x => x.Quantity);
            TotalAmount = SoldItemList.Sum(x => x.Amount);
        }

        private void UpdatePaymentInfo()
        {
            TotalAmountWithDue = SelecSaleCustomer.PreviousDue + TotalAmount;
            TotalPayment = CashPayment + PtPayment + BonusPayment;

            FinalBalanceAmount = TotalPayment - TotalAmountWithDue;
            if (FinalBalanceAmount == 0)
            {
                FinalBalanceLbl = "CHANGE";
                FinalBalanceDetail = string.Empty;
            }
            else if (FinalBalanceAmount < 0)
            {
                FinalBalanceLbl = "CHANGE";
                FinalBalanceDetail = "This amount will be saved as due";
            }
            else
            {
                ReturnVisibility = Visibility.Visible;
                FinalBalanceLbl = "CHANGE";
                FinalBalanceDetail = "This amount should be returned.";
            }

        }

        private void UpdateReturnInfo()
        {
            
        }

        #endregion
    }
}
