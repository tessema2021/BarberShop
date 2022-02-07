using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    public class TransactionService
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public List<Service> Services { get; set; }
        public Transaction Transaction { get; set; }
    }
}
