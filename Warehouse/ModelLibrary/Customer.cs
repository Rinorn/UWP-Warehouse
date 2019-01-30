﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Customer
    {
        [Key]
        public int customerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public int zipCode { get; set; }
        public int areaCode { get; set; }
        public int phoneNumber { get; set; }
        //public virtual List<Order> orderHistory { get; set; }
    }
}
