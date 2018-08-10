using System;

namespace WineTime.Models
{
    public class WineOrderProduct
    {
        public Guid ID { get; set; }
        public WineOrder WineOrder { get; set; }
        public Guid WineOrderID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int? ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}