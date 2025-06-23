using KursProjectISP31.Model;
using KursProjectISP31.Services;
using KursProjectISP31.Utills;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace KursProjectISP31.ViewModel
{
    public class OrderTrackingViewModel : ViewModelBase
    {
        private readonly RestaurantService _restaurantService;

        private ObservableCollection<OrderItem> _allOrderItems;
        public ObservableCollection<OrderItem> AllOrderItems
        {
            get => _allOrderItems;
            set { _allOrderItems = value; OnPropertyChanged(); }
        }

        private OrderItem _selectedOrderItem;
        public OrderItem SelectedOrderItem
        {
            get => _selectedOrderItem;
            set { _selectedOrderItem = value; OnPropertyChanged(); }
        }

        public OrderTrackingViewModel()
        {
            _restaurantService = new RestaurantService();
            LoadAllOrderItems();
        }

        private void LoadAllOrderItems()
        {
            var items = _restaurantService.GetAllOrders();
            AllOrderItems = new ObservableCollection<OrderItem>(items);
        }

        public void UpdateSelectedOrderItemStatus(int newStatus)
        {
            if (SelectedOrderItem != null)
            {
                _restaurantService.UpdateOrderItemStatus(SelectedOrderItem.Id, newStatus);
                
                LoadAllOrderItems();
            }
        }
    }
} 