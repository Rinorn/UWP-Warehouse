using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelLibrary
{
    public class Product
    {
        [Key]
        [Required]
        public string description { get; set; }
        //the ? indicates that the variable can be Null
        public int? itemNumber { get; set; }
        public double price { get; set; }
        public int categoryId { get; set; }
        public int inStock { get; set; }
        public int sold { get; set; }
        public virtual List<ProductToOrder> ProductsToOrders { get; set; }
    }
}
