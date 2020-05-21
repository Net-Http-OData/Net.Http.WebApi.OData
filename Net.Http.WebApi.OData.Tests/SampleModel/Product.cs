using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Model
{
    public class Product
    {
        [Required]
        public Category Category { get; set; }

        public Colour Colour { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Discontinued { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int ProductId { get; set; }

        public float Rating { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
