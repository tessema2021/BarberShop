using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<Customer> CustomerList { get; set; }
        public DateTime TransactionDate { get; set; }
        public Service Service { get; set; }
        public int ServiceId { get; set; }
     
    }
}
