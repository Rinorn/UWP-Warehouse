using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelLibrary
{
    public class Category
    {
        [Key]
        [Required]
        public int categoryId { get; set; }
        [Required]
        public string name { get; set; }
        public virtual List<Product> hasProducts { get; set; }

        public string CategoryName => $"{name}";
    }
}
