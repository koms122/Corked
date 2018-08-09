using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineTime.Models
{
    public class WineCart
    {
        public WineCart()
        {
            //This is not necessary but helpful for unit test:
            this.WineCartProducts = new HashSet<WineCartProduct>();
        }

        public int ID { get; set; }
        // COULD do this: public WineProducts[] Products { get; set; }
        // BUT this is better & more flexible; it'll help later on when needed to come out of a database
        public ICollection<WineCartProduct> WineCartProducts { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserID { get; set; }
    }   
}
