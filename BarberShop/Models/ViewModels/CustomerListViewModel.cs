using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Models.ViewModels
{
    public class CustomerListViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<Transaction> Transactions { get; set; }
        public UserProfile UserProfile { get; set; }
       
    }
}
