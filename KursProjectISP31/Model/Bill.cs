using System;

namespace KursProjectISP31.Model
{
    public class Bill
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDateTime { get; set; }
    }
} 