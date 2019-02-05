using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Annotations;

namespace ModelLibrary
{
    public class Order
    {
        [Key]
        [Required]
        public long orderId { get; set; }
        public virtual List<Customer> Customers { get; set; }
        public virtual List<ProductToOrder> ProductsToOrders { get; set; }
    }
}
