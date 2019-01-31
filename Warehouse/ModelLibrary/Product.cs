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
        public int ID { get; set; }
        [Required]
        public string description { get; set; }
        public int? itemNumber { get; set; }
        public double price { get; set; }
        public Category category { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
