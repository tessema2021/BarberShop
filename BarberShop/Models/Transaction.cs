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
        public int CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
