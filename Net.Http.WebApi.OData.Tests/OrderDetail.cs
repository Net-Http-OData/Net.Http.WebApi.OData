namespace NorthwindModel
{
    public class OrderDetail
    {
        public long OrderId
        {
            get;
            set;
        }

        public int ProductId
        {
            get;
            set;
        }

        public short Quantity
        {
            get;
            set;
        }

        public decimal UnitPrice
        {
            get;
            set;
        }
    }
}