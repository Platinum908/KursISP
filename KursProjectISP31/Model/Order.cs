using KursProjectISP31.Utills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjectISP31.Model
{
    public  class Order:ViewModelBase
    {
        private int id;
        public int Id
        {
            get { return id; }
        }
        private DateTime orderDate;
        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; OnPropertyChanged(nameof(OrderDate)); }
        }
        private int orderCount;
        public int OrderCount
        {
            get { return orderCount; }
            set { orderCount = value; OnPropertyChanged(nameof(OrderCount)); }
        }
        private int idProduct;
        public int IdProduct
        {
            get { return idProduct; }
            set { idProduct = value; OnPropertyChanged(nameof(IdProduct)); }
        }
        private int idEmployee;
        public int IdEmployee
        {
            get { return idEmployee; }
            set { idEmployee = value; OnPropertyChanged(nameof(IdEmployee)); }
        }
    }
}
