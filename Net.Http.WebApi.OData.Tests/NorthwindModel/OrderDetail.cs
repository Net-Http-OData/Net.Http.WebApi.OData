namespace NorthwindModel
{
    public class OrderDetail
    {
        public Order Order { get; set; }

        public long OrderId { get; set; }

        public int ProductId { get; set; }

        public short Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
