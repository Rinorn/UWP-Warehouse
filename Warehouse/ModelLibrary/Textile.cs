using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Textile : Product
    {
        //I also think color should be its own class. You could than have color IDs for every shade and so on.
        public string color { get; set; }
    }
}
