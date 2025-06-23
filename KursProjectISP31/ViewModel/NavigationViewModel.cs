using KursProjectISP31.Utills;
using System.Windows.Input;

namespace KursProjectISP31.ViewModel
{
    public class NavigationViewModel:ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand TablesCommand { get; set; }
        public ICommand MenuCommand { get; set; }
        public ICommand BookingCommand { get; set; }
        public ICommand OrderCommand { get; set; }
        public ICommand ReportsCommand { get; set; }
        public ICommand OrderTrackingCommand { get; set; }

        private void ShowTables(object obj) => CurrentView = new TablesViewModel();
        private void ShowMenu(object obj) => CurrentView = new MenuViewModel();
        private void ShowBooking(object obj) => CurrentView = new BookingViewModel();
        private void ShowOrder(object obj) => CurrentView = new OrderViewModel();
        private void ShowReports(object obj) => CurrentView = new ReportsViewModel();
        private void ShowOrderTracking(object obj) => CurrentView = new OrderTrackingViewModel();

        public NavigationViewModel()
        {
            TablesCommand = new RelayCommand(ShowTables);
            MenuCommand = new RelayCommand(ShowMenu);
            BookingCommand = new RelayCommand(ShowBooking);
            OrderCommand = new RelayCommand(ShowOrder);
            ReportsCommand = new RelayCommand(ShowReports);
            OrderTrackingCommand = new RelayCommand(ShowOrderTracking);
     
            CurrentView = new TablesViewModel();
        }
    }
}
