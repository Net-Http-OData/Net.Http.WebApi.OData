using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample.Model
{
    public class Order
    {
        [Required]
        public Customer Customer { get; set; }

        public DateTimeOffset Date { get; set; }

        public decimal Freight { get; set; }

        public IList<OrderDetail> OrderDetails { get; set; }

        public long OrderId { get; set; }

        [Required]
        public string ShipCountry { get; set; }

        public Guid TransactionId { get; set; }
    }
}
