using BarberShop.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;




namespace BarberShop.Repositories
{
    public interface ITransactionServiceRepository
    {
        List<TransactionService> GetByTransactionId(int Id);
        void DeleteTransactionServices(int transactionId);
    }
}