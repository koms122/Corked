using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineTime.Models
{
    public class ScheduleModel
    {
        public WineOrder Order { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? DeliveryTime { get; set; }
    }
}
