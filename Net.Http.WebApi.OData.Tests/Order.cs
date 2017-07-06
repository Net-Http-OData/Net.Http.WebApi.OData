namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public decimal Freight
        {
            get;
            set;
        }

        public IList<OrderDetail> OrderDetails
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