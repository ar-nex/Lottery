using System;
using System.Collections.ObjectModel;
using System.Linq;
using Lottery_v2.Model;
using Lottery_v2.Model.Database;
using Lottery_v2.ViewModel.Commands;
using System.Windows;

namespace Lottery_v2.ViewModel
{
    public class MainWindowProductViewModel : BaseViewModel
    {
        public MainWindowProductViewModel()
            : base()
        {
            startUpInitializer();
        }

        #region property
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

        private int _productIndex;
        public int ProductIndex
        {
            get => _productIndex;
            set
            {
                if (value < -0 || value > ProductList.Count())
                {
                    _productIndex = -1;
                    SelectedProduct = null;
                }
                else
                {
                    _productIndex = value;
                    SelectedProduct = ProductList[value];
                }
                OnPropertyChanged("ProductIndex");
            }
        }

        /// <summary>
        /// Used to send selected product to the edit window
        /// </summary>
        private static Product _seletectedProduct;
        public static Product SelectedProduct
        {
            get => _seletectedProduct;
            set => _seletectedProduct = value;
        }

        public ProductDb Pdb { get; set; }
        public RelayCommand AddBtnClickedCommand { get; set; }
        public RelayCommand EditBtnClickedCommand { get; set; }
        public RelayCommand DeleteBtnClickedCommand { get; set; }
        

        #endregion

        #region method
        private void startUpInitializer()
        {         
            ProductDb Pdb = new ProductDb();
            EventReferencer.AddReference("prodEntry", Pdb);
            ProductList = new ObservableCollection<Product>(Pdb.GetProductList());
            ProductIndex = -1;
            SelectedProduct = new Product();

            AddBtnClickedCommand = new RelayCommand(addBtnClicked, canClickAddBtn);
            EditBtnClickedCommand = new RelayCommand(editBtnClicked, canClickEditBtn);
            DeleteBtnClickedCommand = new RelayCommand(deleteBtnClicked, canClickDeleteBtn);

            Pdb.ProductInsertEvent += UpdateProductList;
            
        }

        private void addBtnClicked()
        {
            Lottery_v2.View.AddProductView apv = new View.AddProductView();
            apv.Owner = Application.Current.MainWindow;
            apv.ShowDialog();
        }
        private bool canClickAddBtn()
        {
            return true;
        }

        private void editBtnClicked()
        {
            View.EditProductView epv = new View.EditProductView();
            epv.Owner = Application.Current.MainWindow;
            epv.ShowDialog();
        }

        private bool canClickEditBtn()
        {
            return (ProductIndex > -1 && ProductIndex < ProductList.Count);
        }

        private void deleteBtnClicked()
        {
            string selecProdId = ProductList[ProductIndex].Id;
            ProductDb db = new ProductDb();
            int delRow = db.DeleteProduct(selecProdId);
            if (delRow == 1)
            {
                System.Windows.MessageBox.Show("Deleted.");
                int selIndex = ProductIndex;
                ProductIndex = selIndex - 1;
                ProductList.RemoveAt(selIndex);
            }

        }

        private bool canClickDeleteBtn()
        {
            return (ProductIndex > -1 && ProductIndex < ProductList.Count);
        }

        public void UpdateProductList(Object sender, EventArgs e)
        {
            ProductIndex = -1;
            ProductDb db = EventReferencer.GetReference("prodEntry");
            Product p = db.GetLastInsertedProduct();
            if (p.Id != "0")
            {
                ProductList.Add(p);
            }
        }
        #endregion
    }
}
