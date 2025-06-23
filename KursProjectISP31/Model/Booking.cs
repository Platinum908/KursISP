using System;

namespace KursProjectISP31.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BookingDateTime { get; set; }
        public double TimeLimitHours { get; set; }
        public string Comment { get; set; }
    }
} 