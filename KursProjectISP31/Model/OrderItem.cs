namespace KursProjectISP31.Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        
        // This property is for UI display only
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }
        public string StatusText
        {
            get
            {
                return Status switch
                {
                    0 => "Ожидает",
                    1 => "Готовится",
                    2 => "Готов",
                    _ => "Неизвестно",
                };
            }
        }
    }
} 