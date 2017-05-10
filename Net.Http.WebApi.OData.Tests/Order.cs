namespace NorthwindModel
{
    using System;

    public class Order
    {
        public decimal Freight
        {
            get;
            set;
        }

        public long OrderId
        {
            get;
            set;
        }

        public string ShipCountry
        {
            get;
            set;
        }

        public Guid TransactionId
        {
            get;
            set;
        }
    }
}