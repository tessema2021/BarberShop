using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Models.ViewModels
{
    public class TransactionFormViewModel
    {
        public Transaction Transaction { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Service> Services { get; set; }
        public List<int> SelectedServiceIds { get; set; }
    }
}
