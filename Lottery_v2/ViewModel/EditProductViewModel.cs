using System;
using System.Linq;
using Lottery_v2.Model;
using Lottery_v2.Model.Database;
using Lottery_v2.ViewModel.Commands;

namespace Lottery_v2.ViewModel
{
    public class EditProductViewModel : BaseViewModel
    {
        public EditProductViewModel()
            : base()
        {
            startUpInitializer();
        }


        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
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

        private string _pname;
        public string PName
        {
            get => _pname;
            set
            {
                _pname = value.ToUpper();
                OnPropertyChanged("PName");
            }
        }

        public string[] TypeList { get; set; }

        private int _typeListIndex;
        public int TypeListIndex
        {
            get => _typeListIndex;
            set
            {
                if (value < -1 || value > TypeList.Length)
                {
                    _typeListIndex = -1;
                }
                else
                {
                    _typeListIndex = value;
                }
                OnPropertyChanged("TypeListIndex");
            }
        }

        private decimal _rate;
        public decimal Rate
        {
            get => _rate;
            set
            {
                _rate = value;
                OnPropertyChanged("Rate");
            }
        }

        public RelayCommand UpdateProductCommand { get; set; }

        private void startUpInitializer()
        {
            TypeList = new string[] { "MORNING", "EVENING", "SPECIAL" };
            SelectedProduct = MainWindowProductViewModel.SelectedProduct;
            fillDetails();
            UpdateProductCommand = new RelayCommand(updateBtnClicked, canClickUpdateBtn);

        }


        private void fillDetails()
        {
            if (SelectedProduct != null)
            {
                Id = SelectedProduct.Id;
                PName = SelectedProduct.Name;
                TypeListIndex = Array.IndexOf(TypeList, SelectedProduct.Type);
                Rate = SelectedProduct.Rate;
                
            }
        }

        private Product getUpdatedProduct()
        {
            Product p = new Product();
            p.Id = SelectedProduct.Id;
            p.Name = PName;
            p.Type = TypeList[TypeListIndex];
            p.Rate = Rate;
            return p;
        }

        private void updateBtnClicked()
        {
            Product p = getUpdatedProduct();
            ProductDb db = new ProductDb();
            int affRow = db.UpdateProduct(p);
            if (affRow == 1)
            {
                SelectedProduct.Name = p.Name;
                SelectedProduct.Type = p.Type;
                SelectedProduct.Rate = p.Rate;
            }
        }

        private bool canClickUpdateBtn()
        {
            return (!string.IsNullOrEmpty(PName) && (TypeListIndex > -1 && TypeListIndex < TypeList.Count()) && Rate > 0);
        }
    }
}
