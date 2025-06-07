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
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
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
                    Message = "Служащий сохранен";
                else
                    Message = "Ошибка сохранения служащего";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region UpdateOperation
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }
        public void Update()
        {
            try
            {
                var IsUpdated = empService.Update(CurrentEmployee);
                LoadData();
                if (IsUpdated)
                    Message = "Служащий обновлен";
                else
                    Message = "Ошибка обновления служащего";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region DeleteOperation
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void Delete()
        {
            try
            {
                var IsDeleted = empService.Delete(CurrentEmployee.Id);
                LoadData();
                if (IsDeleted)
                    Message = "Служащий удален";
                else
                    Message = "Ошибка удаления служащего";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

    }
}
