using System;

namespace Lottery_v2.Model
{
    public class Product : ViewModel.BaseViewModel
    {
        public Product()
        :base()
        {

        }

        private string _id;
        public string Id { get { return this._id; } set { this._id = value; this.OnPropertyChanged("Id"); } }
        
        private string _name;
        public string Name { get { return this._name; } set { this._name = value.ToUpper(); this.OnPropertyChanged("Name"); } }

        private string _type;
        public string Type { get { return this._type; } set { this._type = value.ToUpper(); this.OnPropertyChanged("Type"); } }

        private decimal _rate;
        public decimal Rate { get { return this._rate; } set { this._rate = value; this.OnPropertyChanged("Rate"); } }

        private DateTime _lastUpdated;
        public DateTime LastUpdated { get { return this._lastUpdated; } set { this._lastUpdated = value; this.OnPropertyChanged("LastUpdated"); } }
    }
}
