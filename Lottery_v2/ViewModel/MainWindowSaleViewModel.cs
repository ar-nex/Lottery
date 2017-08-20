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
                TotalAmountWithDue = SelecCustDue + TotalAmount;
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
                decimal vl = decimal.Round(value, 0);
                _totalAmountWithDue = vl;
                OnPropertyChanged("TotalAmountWithDue");
                FinalBalanceAmount = TotalAmountWithDue - TotalPayment;
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
                CalculateTotalPayment();
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
                CalculateTotalPayment();
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
                CalculateTotalPayment();
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
                FinalBalanceAmount = TotalAmountWithDue - TotalPayment;
            }
        }


       

        private string _finalMsg;
        public string FinalMsg
        {
            get => _finalMsg;
            set
            {
                _finalMsg = value;
                OnPropertyChanged("finalMsg");
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
                UpdateVisibility();
                CalculatefinalDue();
            }
        }

        private decimal _returnAmount;
        public decimal ReturnAmount
        {
            get => _returnAmount;
            set
            {
                _returnAmount = (value > -FinalBalanceAmount || value < 0) ? 0 : value;
                //_returnAmount = value;
                OnPropertyChanged("ReturnAmount");
                CalculatefinalDue();
            }
        }

        private decimal _finalDue;
        public decimal FinalDue
        {
            get => _finalDue;
            set
            {
                _finalDue = value;
                OnPropertyChanged("FinalDue");
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
        public SaleDb sdb { get; set; }
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

            SaleDb sdb = new SaleDb();
            EventReferencerSale.AddReference("saleEntry", sdb);
            sdb.SaleInsertEvent += Sdb_SaleInsertEvent;

            FinalBalanceAmount = 0;
            FinalMsg = string.Empty;

            ReturnVisibility = Visibility.Hidden;

            AddSoldItemCommand = new RelayCommand(AddSoldItemBtnClicked, CanClickAddSoldItemBtn);
            RemoveSoldItemCommand = new RelayCommandWithParam(RemoveSoldItem, CanRemoveSoldItem);

        }

        private void Sdb_SaleInsertEvent(object sender, EventArgs e)
        {
            // get the customer updated due and 
            throw new NotImplementedException();
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
                TotalAmountWithDue = SelecSaleCustomer.PreviousDue + TotalAmount;
            }
            else
            {
                SaleCustId = string.Empty;
                SaleCustAgency = string.Empty;
                SelecCustDue = 0;
                TotalAmountWithDue = SelecCustDue + TotalAmount;
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

        private void CalculateTotalPayment()
        {
            TotalPayment = CashPayment + PtPayment + BonusPayment;
        }

        private void UpdateVisibility()
        {
            ReturnVisibility = (FinalBalanceAmount >= 0) ? Visibility.Hidden : Visibility.Visible;
        }

        private void CalculatefinalDue()
        {
            decimal fdue = 0;
            if (FinalBalanceAmount == 0)
            {
                fdue = 0;
                FinalMsg = string.Empty;
            }
            else if (FinalBalanceAmount < 0)
            {

                if (ReturnAmount == 0)
                {
                    fdue = FinalBalanceAmount;
                    FinalMsg = "Rs. " + (-fdue).ToString() + " will be saved as credit";
                }
                else if (ReturnAmount > 0)
                {
                    fdue = (FinalBalanceAmount + ReturnAmount);
                    if (fdue == 0)
                        FinalMsg = string.Empty;
                    else
                        FinalMsg = "Rs. " + (-fdue).ToString() + "will be saved as credit";
                }
                
            }
            else
            {
                fdue = FinalBalanceAmount;
                FinalMsg = "Rs. " + fdue.ToString() + " will be saved as due";
            }
            FinalDue = fdue;
        }

        private void CommitTransaction()
        {
            // insert it into the Db
            // raise the event and update cusotmer due in two classes 1. sale 2. customer
            // have to get the reference of sale db both in sale and in customer

            // calculate final due
            CalculatefinalDue();
            Sale sl = new Sale();
            sl.CustId = SaleCustId;
            sl.SoldItems = SoldItemList.ToList();
            sl.CashPayment = CashPayment;
            sl.PtPayment = PtPayment;
            sl.BonusPayment = BonusPayment;
            sl.CustCurrDue = FinalDue;

        }

       

        private bool CanCommitTransaction()
        {
            bool validCustInfo = (SelecSaleCustomer != null) && (CustomerList.IndexOf(SelecSaleCustomer) != -1);
            bool SoldItemNotEmpty = SoldItemList.Count() > 0;
            return validCustInfo && SoldItemNotEmpty;
        }

        #endregion
    }
}
