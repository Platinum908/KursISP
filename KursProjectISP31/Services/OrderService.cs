using KursProjectISP31.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjectISP31.Services
{
    public class OrderService : BaseService<Order>
    {
        public OrderService() : base()
        {
        }
        public override bool Add(Order obj)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Order> GetAll()
        {
            List<Order> list = new List<Order>();
            return list;
        }

        public override bool Update(Order obj)
        {
            throw new NotImplementedException();
        }
    }
}
