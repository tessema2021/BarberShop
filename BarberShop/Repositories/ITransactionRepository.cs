using System.Collections.Generic;
using BarberShop.Models;

namespace BarberShop.Repositories
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAllTransactions();
    }
}