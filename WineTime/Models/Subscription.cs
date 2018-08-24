using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineTime.Models
{
    public class Subscription
    {
        public string Email { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
