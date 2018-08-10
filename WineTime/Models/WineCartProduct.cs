using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineTime.Models
{
    public class WineCartProduct
    {
        public int ID { get; set; }
        public WineCart WineCart { get; set; }
        public int WineCartID { get; set; }
        public WineProducts WineProducts { get; set; }
        public int WineProductsID { get; set; }
        public int? Quantity { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
