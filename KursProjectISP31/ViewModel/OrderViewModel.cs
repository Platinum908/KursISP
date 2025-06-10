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
    internal class OrderViewModel:ViewModelBase
    {
        private OrderService orderService;
        private EmployeeService employeeService;
        #region DisplayOperation
        private ObservableCollection<Order> orderList;
        public ObservableCollection<Order> OrderList
        {
            get => orderList;
            set { orderList = value; OnPropertyChanged(nameof(OrderList)); }
        }
        private List<Employee> employeeList=new();
        public List<Employee> EmployeeList
        {
            get => employeeList;
            set { employeeList = value; OnPropertyChanged(nameof(EmployeeList)); }
        }
        private void LoadData()
        {
            OrderList = new ObservableCollection<Order>(orderService.GetAll());
            EmployeeList = employeeService.GetAll();
        }
        #endregion
        private Order currentOrder;
        public Order CurrentOrder
        {
            get { return currentOrder; }
            set { currentOrder = value; OnPropertyChanged(nameof(CurrentOrder)); }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(nameof(Message)); }
        }
        public OrderViewModel()
        {
            orderService = new OrderService();
            employeeService = new EmployeeService();
            LoadData();
            CurrentOrder = new Order();
            saveCommand = new RelayCommandSQL(Save);
            updateCommand = new RelayCommandSQL(Update);
            deleteCommand = new RelayCommandSQL(Delete);
        }

        #region SaveOperation
        private RelayCommandSQL saveCommand;
        public RelayCommandSQL SaveCommand
        {
            get { return saveCommand; }
        }
        public void Save()
        {
            try
            {
                var IsSaved = orderService.Add(CurrentOrder);
                LoadData();
                if (IsSaved)
                    Message = "Заказ сохранен";
                else
                    Message = "Ошибка сохранения заказа";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region UpdateOperation
        private RelayCommandSQL updateCommand;
        public RelayCommandSQL UpdateCommand
        {
            get { return updateCommand; }
        }
        public void Update()
        {
            try
            {
                var IsUpdated = orderService.Update(CurrentOrder);
                LoadData();
                if (IsUpdated)
                    Message = "Заказ обновлен";
                else
                    Message = "Ошибка обновления заказ";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region DeleteOperation
        private RelayCommandSQL deleteCommand;
        public RelayCommandSQL DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void Delete()
        {
            try
            {
                var IsDeleted = orderService.Delete(CurrentOrder.Id);
                LoadData();
                if (IsDeleted)
                    Message = "Заказ удален";
                else
                    Message = "Ошибка удаления заказа";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
    }
}
