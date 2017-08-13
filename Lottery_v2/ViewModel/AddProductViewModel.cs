using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottery_v2.Model;
using Lottery_v2.Model.Database;
using Lottery_v2.ViewModel.Commands;
namespace Lottery_v2.ViewModel
{
    public class AddProductViewModel : BaseViewModel
    {
        public AddProductViewModel()
            : base()
        {
            startUpInitializer();
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

        public RelayCommand AddProductCommand { get; set; }

        private void startUpInitializer()
        {
            TypeList = new string[] { "MORNING", "EVENING", "SPECIAL"};
            TypeListIndex = -1;
            AddProductCommand = new RelayCommand(addBtnClicked, canClickAddBtn);
        }

        private void addBtnClicked()
        {
            Product p = new Product();
            p.Name = PName;
            p.Type = TypeList[TypeListIndex];
            p.Rate = Rate;
            ProductDb db = Model.EventReferencer.GetReference("prodEntry");
            int id = db.InsertProduct(p);
            if (id > 0)
            {
                System.Windows.MessageBox.Show("Saved Successfully.");
                PName = string.Empty;
                TypeListIndex = -1;
                Rate = 0;
            }
        }
        private bool canClickAddBtn()
        {
            return (!string.IsNullOrEmpty(PName) && (TypeListIndex > -1 && TypeListIndex < TypeList.Count()) && Rate > 0);
        }
    }
}
