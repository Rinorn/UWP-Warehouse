using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ProductToOrder
    {
        public int prodToOrderId { get; set; }
        public string prodDescription { get; set; }
        public virtual Product product { get; set; }
        public int quantity { get; set; }
        public long orderId { get; set; }
        public virtual Order order { get; set; }
    }
}
