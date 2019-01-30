using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Member : Customer
    {
        public int membershipId { get; set; }
        [Required]
        public virtual List<Category> discounts { get; set; }
    }
}
