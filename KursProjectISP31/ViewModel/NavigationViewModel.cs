using KursProjectISP31.Utills;
using KursProjectISP31.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand HomeCommand { get; set; }
        public ICommand EmployeeCommand { get; set; }
        public ICommand OrderCommand { get; set; }
        private void HomePage(object obj) => CurrentView = new HomeViewModel();
        private void EmployeePage(object obj) => CurrentView = new EmployeeViewModel();
        private void OrderPage(object obj) => CurrentView = new OrderViewModel();
        public NavigationViewModel()
        {
            HomeCommand = new RelayCommand(HomePage);
            EmployeeCommand = new RelayCommand(EmployeePage);
            OrderCommand = new RelayCommand(OrderPage);
            // Startup Page
            CurrentView = new HomeViewModel();
        }
    }
}
