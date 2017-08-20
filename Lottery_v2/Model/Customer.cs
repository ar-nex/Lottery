using System;

namespace Lottery_v2.Model
{
    public class Customer : ViewModel.BaseViewModel
    {
        public Customer()
        : base()
        {

        }
        private string _id;
        public string Id { get { return this._id; } set { this._id = value; this.OnPropertyChanged("Id"); } }

        private string _name;
        public string Name { get { return this._name; } set { this._name = value; this.OnPropertyChanged("Name"); } }

        private string _agency;
        public string Agency { get { return this._agency; } set { this._agency = value; this.OnPropertyChanged("Agency"); } }

        private string _mobile;
        public string Mobile { get { return this._mobile; } set { this._mobile = value; this.OnPropertyChanged("Mobile"); } }

        private string _address;
        public string Address { get { return this._address; } set { this._address = value; this.OnPropertyChanged("Address"); } }

        private DateTime _joiningDate;
        public DateTime JoiningDate { get { return this._joiningDate; } set { this._joiningDate = value; this.OnPropertyChanged("JoiningDate"); } }

        private decimal _previousDue;
        public decimal PreviousDue { get { return this._previousDue; } set { this._previousDue = value; this.OnPropertyChanged("PreviousDue"); } }

        private DateTime _dueUpdatedOn;
        public DateTime DueUpdatedOn
        {
            get => _dueUpdatedOn;
            set
            {
                DueUpdatedOn = value;
                OnPropertyChanged("DueUpdatedOn");
            }
        }

    }
}
