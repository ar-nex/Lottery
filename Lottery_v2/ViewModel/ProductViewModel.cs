using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Lottery_v2.Model;
using Lottery_v2.Model.Database;
using Lottery_v2.ViewModel.Commands;

namespace Lottery_v2.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {
        public ProductViewModel()
        : base()
        {
            this.startUpInitializer();
        }

        #region property
        private ObservableCollection<Product> _productGridList;
        public ObservableCollection<Product> ProductGridList
        {
            get { return this._productGridList; }
            set { this._productGridList = value; this.OnPropertyChanged("ProductGridList"); }
        }

        private int _productGridListIndex;
        public int ProductGridListIndex
        {
            get { return this._productGridListIndex; }
            set 
            {
                if (value < -1 || value >= this.ProductGridList.Count)
                {
                    this._productGridListIndex = -1;
                }
                else
                {
                    this._productGridListIndex = value;
                }
                this.FillFormFieldsFromGrid();
                this.OnPropertyChanged("ProductGridListIndex");
                this.ArrProductTypesIndex = (this.ProductGridListIndex == -1) ? -1 : Array.IndexOf(this.ArrProductTypes, this.ProductGridList[this.ProductGridListIndex].Type);
            }
        }

        public string[] ArrProductTypes { get; set; }

        private int _arrProductTypesIndex;
        public int ArrProductTypesIndex 
        {
            get { return this._arrProductTypesIndex; }
            set 
            {
                if (value < -1 || value >= this.ArrProductTypes.Length)
                {
                    this._arrProductTypesIndex = -1;
                }
                else 
                {
                    this._arrProductTypesIndex = value; 
                }             
                this.OnPropertyChanged("ArrProductTypesIndex");
                this.Type = (this.ArrProductTypesIndex == -1) ? string.Empty : this.ArrProductTypes[this.ArrProductTypesIndex];
            }
        }

        private string _id;
        public string Id
        {
            get { return this._id; }
            set { this._id = value; this.OnPropertyChanged("Id"); }
        }

        private string _name;
        public string Name
        {
            get { return this._name; }
            set { this._name = value.ToUpper(); this.OnPropertyChanged("Name"); }
        }

        private string _type;
        public string Type
        {
            get { return this._type; }
            set { this._type = value.ToUpper(); this.OnPropertyChanged("Type"); }
        }

        private decimal _rate;
        public decimal Rate
        {
            get { return this._rate; }
            set { this._rate = value; this.OnPropertyChanged("Rate"); }
        }

        private DateTime _lastUpdatedOn;
        public DateTime LastUpdatedOn
        {
            get { return this._lastUpdatedOn; }
            set { this._lastUpdatedOn = value; this.OnPropertyChanged("LastUpdatedOn"); }
        }

        private enum commandType { add, edit}
        private commandType cmdType;

        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand SaveProductCommand { get; set; }
        public RelayCommand DeleteProductCommand { get; set; }
        #endregion

        #region methods
        private void startUpInitializer()
        {
            ProductDb db = new ProductDb();
            this.ProductGridList = new ObservableCollection<Product>(db.GetProductList());
            this.ArrProductTypes = new string[] { "MORNING", "EVENING", "SPECIAL" };
            this.ProductGridListIndex = -1;
            this.ArrProductTypesIndex = -1;
            this.cmdType = new commandType();

            this.SaveProductCommand = new RelayCommand(this.saveProductClicked, this.canSaveProductClicked);
        }

        private void FillFormFieldsFromGrid()
        {
            if (this.ProductGridListIndex == -1)
            {
                this.Id = string.Empty;
                this.Name = string.Empty;
                this.Rate = 0;
                this.LastUpdatedOn = default(DateTime);
            }
            else
            {
                Product p = this.ProductGridList[this.ProductGridListIndex];
                this.Id = p.Id;
                this.Name = p.Name;
                this.Rate = p.Rate;
                this.LastUpdatedOn = p.LastUpdated;
                
            }
        }

        private Product getNewProductFromFields(commandType cmdType)
        {
            Product p = new Product();
            if (cmdType == commandType.add)
            {
                p.Name = this.Name;
                p.Type = this.Type;
                p.Rate = this.Rate;
            }
            else if (cmdType == commandType.edit)
            {
                p.Id = this.Id;
                p.Name = this.Name;
                p.Type = this.Type;
                p.Rate = this.Rate;

            }
            return p;
        }

        private void addProductClicked()
        {
            this.ProductGridListIndex = -1;
        }
        private bool canAddProductClick()
        {
            return true;
        }


        private void saveProductClicked()
        {
            ProductDb db = new ProductDb();
            if (this.ProductGridListIndex == -1)
            {         
                Product p = this.getNewProductFromFields(commandType.add);
                int insertedId = db.InsertProduct(p);
                if (insertedId != 0)
                {
                    p.Id = insertedId.ToString();
                    this.ProductGridList.Add(p);
                    this.ProductGridListIndex = -1;
                }
                else
                {
                    System.Windows.MessageBox.Show("Something went wrong while adding product.");
                }
            }
            else
            {
                Product p = this.getNewProductFromFields(commandType.edit);
            }
        }

        private bool canSaveProductClicked()
        {
            return !string.IsNullOrEmpty(this.Name);
        }
        #endregion
    }
}
