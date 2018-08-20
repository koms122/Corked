using System;
using System.ComponentModel.DataAnnotations;

namespace WineTime.Models
{
    public class PaymentModel
    {
        public WineOrder Order { get; set; }
        public Guid ID { get; set; }
        [Required]
        public string Nonce { get; set; }
    }
}