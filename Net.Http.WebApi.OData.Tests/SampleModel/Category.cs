using System.ComponentModel.DataAnnotations;

namespace Sample.Model
{
    public class Category
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
