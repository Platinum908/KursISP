using KursProjectISP31.Model;
using KursProjectISP31.Services;
using KursProjectISP31.Utills;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace KursProjectISP31.ViewModel
{
    public class OrderViewModel : ViewModelBase
    {
        private readonly RestaurantService _restaurantService;

        public ObservableCollection<RestaurantTable> AvailableTables { get; set; }
        public ObservableCollection<MenuItem> MenuItems { get; set; }
        public ObservableCollection<OrderItem> CurrentOrderItems { get; set; }

        private Order _newOrder;
        public Order NewOrder { get => _newOrder; set { _newOrder = value; OnPropertyChanged(); } }

        private RestaurantTable _selectedTable;
        public RestaurantTable SelectedTable { get => _selectedTable; set { _selectedTable = value; OnPropertyChanged(); } }

        private MenuItem _selectedMenuItem;
        public MenuItem SelectedMenuItem { get => _selectedMenuItem; set { _selectedMenuItem = value; OnPropertyChanged(); } }
        
        private int _quantity = 1;
        public int Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(); } }

        private string _message;
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
        
        public decimal OrderTotal => CurrentOrderItems.Sum(item => item.Total);

        public ICommand CreateOrderCommand { get; set; }
        public ICommand AddToOrderCommand { get; set; }
        public ICommand GenerateBillCommand { get; set; }

        public OrderViewModel()
        {
            _restaurantService = new RestaurantService();
            
            CreateOrderCommand = new RelayCommand(CreateOrder, CanCreateOrder);
            AddToOrderCommand = new RelayCommand(AddToOrder, CanAddToOrder);
            GenerateBillCommand = new RelayCommand(GenerateBill, CanGenerateBill);

            LoadInitialData();
        }

        private void LoadInitialData()
        {
            AvailableTables = new ObservableCollection<RestaurantTable>(_restaurantService.GetAvailableTables());
            MenuItems = new ObservableCollection<MenuItem>(_restaurantService.GetMenu());
            CurrentOrderItems = new ObservableCollection<OrderItem>();
            NewOrder = new Order { Id = _restaurantService.GetNextOrderId() };
            CurrentOrderItems.CollectionChanged += (s, e) => OnPropertyChanged(nameof(OrderTotal));
        }

        private bool CanCreateOrder(object obj) => SelectedTable != null && NewOrder.TableId == 0;
        private void CreateOrder(object obj)
        {
            NewOrder.TableId = SelectedTable.Id;
            NewOrder.OrderDateTime = DateTime.Now;

            try
            {
                if (_restaurantService.MakeOrder(NewOrder))
                {
                    Message = $"Заказ {NewOrder.Id} для столика {NewOrder.TableId} создан.";
                }
                else Message = "Не удалось создать заказ.";
            }
            catch (Exception ex) { Message = $"Ошибка: {ex.Message}"; }
        }

        private bool CanAddToOrder(object obj) => NewOrder.TableId != 0 && SelectedMenuItem != null && Quantity > 0;
        private void AddToOrder(object obj)
        {
            var newOrderItem = new OrderItem
            {
                Id = _restaurantService.GetNextOrderItemId(),
                MenuItemId = SelectedMenuItem.Id,
                OrderId = NewOrder.Id,
                Quantity = this.Quantity,
                Status = 0,
                Name = SelectedMenuItem.Name,
                Price = SelectedMenuItem.Price
            };

            try
            {
                if (_restaurantService.AddInOrder(newOrderItem))
                {
                    CurrentOrderItems.Add(newOrderItem); 
                    Message = $"Добавлено: {SelectedMenuItem.Name} x{Quantity}.";
                }
                else Message = "Не удалось добавить позицию.";
            }
            catch (Exception ex) { Message = $"Ошибка: {ex.Message}"; }
        }

        private bool CanGenerateBill(object obj) => NewOrder.TableId != 0 && CurrentOrderItems.Any();
        private void GenerateBill(object obj)
        {
            var newBill = new Bill
            {
                Id = _restaurantService.GetNextBillId(),
                OrderId = NewOrder.Id,
                TableId = NewOrder.TableId,
                TotalAmount = OrderTotal,
                PaymentDateTime = DateTime.Now
            };

            try
            {
                if (_restaurantService.SaveBill(newBill))
                {
                    Message = $"Счет на сумму {OrderTotal:C} для заказа {NewOrder.Id} успешно создан.";
                    
                    LoadInitialData();
                }
                else
                {
                    Message = "Не удалось создать счет.";
                }
            }
            catch (Exception ex)
            {
                Message = $"Ошибка при создании счета: {ex.Message}";
            }
        }
    }
} 