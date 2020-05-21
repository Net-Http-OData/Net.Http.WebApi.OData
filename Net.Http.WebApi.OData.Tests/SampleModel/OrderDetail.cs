namespace Sample.Model
{
    public class OrderDetail
    {
        public decimal Discount { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

        public short Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
