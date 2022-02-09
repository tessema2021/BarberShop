using System.Collections.Generic;
using BarberShop.Models;

namespace BarberShop.Repositories
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAllTransactionsByUser(int id);
        void AddTransaction(Transaction transaction);
        Transaction GetById(int Id);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(int transactionId);
        List<Transaction> GetByCustomerId(int Id);
        void CreateTransactionService(int serviceId, int transactionId);
       
    }
}