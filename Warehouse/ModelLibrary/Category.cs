using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Category
    {
        [Key]
        [Required]
        public int categoryId { get; set; }
        [Required]
        public string name { get; set; }
        public virtual List<Customer> hasDiscount { get; set; }
        public virtual List<Product> hasProducts { get; set; }
    }
}
