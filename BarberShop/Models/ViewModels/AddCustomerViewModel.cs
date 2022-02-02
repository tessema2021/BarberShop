using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Models.ViewModels
{
    public class AddCustomerViewModel
    {
        public Customer Customer { get; set; }
        public Service Service { get; set; }
        public List<Service> Services { get; set; }
    }
}
