using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Models.ViewModels
{
    public class CustomerDropDownViewModel
    {
        public List<Service> ServiceIds { get; set; }
        public List<UserProfile> UserIds { get; set; }
        public int selectedUser { get; set; }
        public int selectedService { get; set; }
        public List<Customer> Customers { get; set; }
        public Customer Customer { get; set; }
    }
}
