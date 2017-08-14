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
            startUpInitializer();
        }

        #region property
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
        #endregion

        #region method
        private void startUpInitializer()
        {
            SoldItemList = new ObservableCollection<SoldItem>();
        }
        #endregion
    }
}
