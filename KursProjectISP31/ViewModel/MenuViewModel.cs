using KursProjectISP31.Model;
using KursProjectISP31.Services;
using KursProjectISP31.Utills;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Linq;

namespace KursProjectISP31.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly RestaurantService _restaurantService;
        public ICollectionView MenuItems { get; private set; }

        public MenuViewModel()
        {
            _restaurantService = new RestaurantService();
            LoadMenu();
        }

        private void LoadMenu()
        {
            var menuList = _restaurantService.GetMenu();
            var menuCollection = new ObservableCollection<MenuItem>(menuList);
            MenuItems = CollectionViewSource.GetDefaultView(menuCollection);
            MenuItems.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
        }
    }
} 