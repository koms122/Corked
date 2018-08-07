using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineTime.Models
{
    public class WineCategory
    {
        public WineCategory()
        {
            // If you create a new WineCategory object, 
            // this ensures that the newly created category 
            // doesn't have a "NULL" value for products
            this.WineProduct = new HashSet<WineProducts>();
        }

        //No ID column b/c EF won't automatically be able to figure out
        //which field to use as the primary key on the table

        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }

        public ICollection<WineProducts> WineProduct { get;set; }
    }
}
