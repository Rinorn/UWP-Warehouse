using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ModelLibrary
{
    public class Product
    {
        [Key]
        [Required]
        public string description { get; set; }
        public int? itemNumber { get; set; }
        public double price { get; set; }
        public int categoryId { get; set; }
        public int inStock { get; set; }
        public int Sold { get; set; }
        public virtual List<ProductToOrder> ProductsToOrders { get; set; }
    }
}
