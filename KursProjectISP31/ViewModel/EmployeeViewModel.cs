using KursProjectISP31.Model;
using KursProjectISP31.Services;
using KursProjectISP31.Utills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjectISP31.ViewModel
{
    public class EmployeeViewModel:ViewModelBase
    {
        private EmployeeService empService;

        #region DisplayOperation
        private ObservableCollection<Employee> empList;
        public ObservableCollection<Employee> EmpList
        {
            get=>empList;
            set { empList = value; OnPropertyChanged(nameof(EmpList)); }
        }
        private void LoadData()
        {
            EmpList = new ObservableCollection<Employee>(empService.GetAll());
        }
        #endregion

        private Employee currentEmployee;
        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value; OnPropertyChanged(nameof(CurrentEmployee)); }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(nameof(Message)); }
        }
        public EmployeeViewModel()
        {
            empService = new EmployeeService();
            LoadData();
            CurrentEmployee = new Employee();
            saveCommand = new RelayCommand(Save);
        }

        #region SaveOperation
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }

        }
        public void Save()
        {
            try
            {
                var IsSaved = empService.Add(CurrentEmployee);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

    }
}
