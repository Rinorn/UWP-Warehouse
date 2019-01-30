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
        public string description { get; set; }
        public int price { get; set; }
        public Category category { get; set; }
        
    }
}
