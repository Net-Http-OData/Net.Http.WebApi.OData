using System;

namespace NorthwindModel
{
    public class Product
    {
        public Category Category { get; set; }

        public Colour Colour { get; set; }

        public bool Deleted { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int ProductId { get; set; }

        public int Rating { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
