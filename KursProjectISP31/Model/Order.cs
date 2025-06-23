using System;

namespace KursProjectISP31.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
} 