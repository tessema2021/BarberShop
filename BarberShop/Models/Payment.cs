using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public List<Service> Services { get; set; }
        public int UserProfileId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string PaymentType { get; set; }
     
    }
}
