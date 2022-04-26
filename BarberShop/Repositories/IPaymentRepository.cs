using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Repositories
{
   public interface IPaymentRepository
    {
         List<Payment> GetAllPayments();
    }
}
