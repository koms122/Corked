using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineTime.Models
{
    public class WineProducts
    {
        public WineProducts()
        {
            this.WineCartProducts = new HashSet<WineCartProduct>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime? Schedule { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public WineCategory WineCategory { get; set; }
        public string WineCategoryName { get; set; }
        public ICollection<WineCartProduct> WineCartProducts { get; set; }
    }
}
