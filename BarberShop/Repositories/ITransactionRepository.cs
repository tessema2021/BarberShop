using System.Collections.Generic;
using BarberShop.Models;

namespace BarberShop.Repositories
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAllTransactions();
        void AddTransaction(Transaction transaction);
        Transaction GetById(int Id);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(int transactionId);
    }
}