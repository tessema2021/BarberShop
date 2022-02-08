using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    public class Service
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Price")]
        public int Cost { get; set; }
    }
}
