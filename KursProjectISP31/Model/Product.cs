using KursProjectISP31.Utills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjectISP31.Model
{
    public class Product:ViewModelBase
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }
        private string productName;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; OnPropertyChanged(nameof(ProductName)); }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; OnPropertyChanged(nameof(Type)); }
        }
        private int rating;
        public int Rating
        {
            get { return rating; }
            set { rating = value; OnPropertyChanged(nameof(Rating)); }
        }
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(nameof(Price)); }
        }
        private string photo;
        public string Photo
        {
            get { return photo; }
            set { photo = value; OnPropertyChanged(nameof(Photo)); }
        }
    }
}
