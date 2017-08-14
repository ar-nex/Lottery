using System;
using System.Linq;
using Lottery_v2.Model;
using Lottery_v2.Model.Database;
using Lottery_v2.ViewModel.Commands;

namespace Lottery_v2.ViewModel
{
    public class EditCustomerViewModel : BaseViewModel
    {
        public EditCustomerViewModel()
            : base()
        {
            startUpInitializer();
        }

        #region property
        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
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

        public RelayCommand UpdateCustCommand { get; set; }
        #endregion

        #region Method
        private void startUpInitializer()
        {
            SelectedCustomer = MainWindowCustomerViewModel.SelectedCustomer;
            fillDetails();
            UpdateCustCommand = new RelayCommand(updateBtnClicked, canClickUpdateBtn);

        }

        private void updateBtnClicked()
        {
            Customer c = getUpdatedCustomer();
            CustomerDb db = new CustomerDb();
            int affRow = db.UpdateCustomer(c);
            if (affRow == 1)
            {
                SelectedCustomer.Name = c.Name;
                SelectedCustomer.Agency = c.Agency;
                SelectedCustomer.Address = c.Address;
                SelectedCustomer.Mobile = c.Mobile;
                SelectedCustomer.JoiningDate = c.JoiningDate;
                SelectedCustomer.PreviousDue = c.PreviousDue;

                System.Windows.MessageBox.Show("Updated Successfully.");
            }
        }
        private bool canClickUpdateBtn()
        {
            return (!string.IsNullOrEmpty(CName));
        }

        private void fillDetails()
        {
            if (SelectedCustomer != null)
            {
                Id = SelectedCustomer.Id;
                CName = SelectedCustomer.Name;
                Agency = SelectedCustomer.Agency;
                Address = SelectedCustomer.Address;
                Mobile = SelectedCustomer.Mobile;
                if (SelectedCustomer.JoiningDate.Year == 1)
                {
                    JoiningDate = new DateTime(2010, 1, 1);
                }
                else
                {
                    JoiningDate = SelectedCustomer.JoiningDate;
                }
                PrevDue = SelectedCustomer.PreviousDue;
            }
        }

        private Customer getUpdatedCustomer()
        {
            Customer c = new Customer();
            c.Id = SelectedCustomer.Id;
            c.Name = CName;
            c.Agency = Agency;
            c.Address = Address;
            c.Mobile = Mobile;
            c.JoiningDate = JoiningDate;
            c.PreviousDue = PrevDue;
            return c;
        }
        #endregion


    }
}
