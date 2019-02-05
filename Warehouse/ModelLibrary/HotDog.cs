using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class HotDog : Product
    {   
        //flavor should be its own class Flavor. Adding it as a string is not a good solution, but im out of time, so it will have to do for now
        public string flavor { get; set; }
    }
}
