using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottery_v2.Model;
using Lottery_v2.Model.Database;
using Lottery_v2.ViewModel.Commands;

namespace Lottery_v2.ViewModel
{
    public class AddCustomerViewModel : BaseViewModel
    {
        public AddCustomerViewModel()
            : base()
        {
            startUpInitializer();
            JoiningDate = DateTime.Today;
        }

        #region property
        private string _cName;
        public string CName
        {
            get => _cName;
            set
            {
                _cName = value.ToUpper();
                OnPropertyChanged("CName");
            }
        }

        private string _agency;
        public string Agency
        {
            get => _agency;
            set
            {
                _agency = value.ToUpper();
                OnPropertyChanged("Agency");
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value.ToUpper();
                OnPropertyChanged("Address");
            }
        }
        private string _mobile;
        public string Mobile
        {
            get => _mobile;
            set
            {
                _mobile = value;
                OnPropertyChanged("Mobile");
            }
        }

        private DateTime _joiningDate;
        public DateTime JoiningDate
        {
            get => _joiningDate;
            set
            {
                _joiningDate = value;
                OnPropertyChanged("JoiningDate");
            }
        }

        private decimal _prevDue;
        public decimal PrevDue
        {
            get => _prevDue;
            set
            {
                _prevDue = value;
                OnPropertyChanged("PrevDue");
            }
        }
        
        public RelayCommand AddCustCommand { get; set; }

        #endregion

        #region method
        private void startUpInitializer()
        {
            AddCustCommand = new RelayCommand(addBtnClicked, canClickAddBtn);
        }

        private void addBtnClicked()
        {
            Customer c = new Customer();
            c.Name = CName;
            c.Agency = Agency;
            c.Address = Address;
            c.Mobile = Mobile;
            c.JoiningDate = JoiningDate;
            c.PreviousDue = PrevDue;
            CustomerDb db = EventReferencerCustomer.GetReference("custEntry");
            int id = db.AddCustomer(c);
            if (id > 0)
            {
                System.Windows.MessageBox.Show("Saved Successfully.");
                CName = string.Empty;
                Agency = string.Empty;
                Address = string.Empty;
                Mobile = string.Empty;
                PrevDue = 0;
            }
        }

        private bool canClickAddBtn()
        {
            return (!string.IsNullOrEmpty(CName));
        }
        #endregion

    }
}
