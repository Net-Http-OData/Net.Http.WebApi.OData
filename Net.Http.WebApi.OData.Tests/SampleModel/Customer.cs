using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample.Model
{
    public class Customer
    {
        [Required]
        public Employee AccountManager { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        [Required]
        public string Country { get; set; }

        public int LegacyId { get; set; }

        public IList<Order> Orders { get; set; }

        public string Phone { get; set; }

        [Required]
        public string PostalCode { get; set; }
    }
}
