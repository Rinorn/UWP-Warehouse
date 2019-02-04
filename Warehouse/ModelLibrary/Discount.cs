using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Discount
    {
        public int discountId { get; set; }
        public int categoryId { get; set; }
        public double percentage { get; set; }
        public List<Customer> hasDiscount { get; set; }
    }
}
