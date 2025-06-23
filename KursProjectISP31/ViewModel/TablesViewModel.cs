using KursProjectISP31.Model;
using KursProjectISP31.Services;
using KursProjectISP31.Utills;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KursProjectISP31.ViewModel
{
    public class TablesViewModel : ViewModelBase
    {
        private ObservableCollection<RestaurantTable> _availableTables;
        public ObservableCollection<RestaurantTable> AvailableTables
        {
            get { return _availableTables; }
            set
            {
                _availableTables = value;
                OnPropertyChanged(nameof(AvailableTables));
            }
        }

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(); }
        }

        private readonly RestaurantService _restaurantService;
        public ICommand FindTablesCommand { get; }

        public TablesViewModel()
        {
            _restaurantService = new RestaurantService();
            FindTablesCommand = new RelayCommand(FindTables);
            LoadCurrentAvailableTables();
        }

        private void LoadCurrentAvailableTables()
        {
            var tables = _restaurantService.GetAvailableTables();
            AvailableTables = new ObservableCollection<RestaurantTable>(tables);
        }

        private void FindTables(object obj)
        {
            var tables = _restaurantService.GetAvailableTablesOnDate(SelectedDate);
            AvailableTables = new ObservableCollection<RestaurantTable>(tables);
        }
    }
} 