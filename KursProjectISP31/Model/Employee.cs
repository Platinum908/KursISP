using KursProjectISP31.Utills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjectISP31.Model
{
    public class Employee:ViewModelBase
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }
        private string nameEmp;
        public string NameEmp
        {
            get { return nameEmp; }
            set { nameEmp = value; OnPropertyChanged(nameof(NameEmp)); }
        }
        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged(nameof(Age)); }
        }
    }
}
