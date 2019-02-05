using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
