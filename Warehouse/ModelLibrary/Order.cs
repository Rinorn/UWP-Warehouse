﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Order
    {

        [Key]
        [Required]
        public int orderId { get; set; }
        public virtual List<Customer> customer { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
