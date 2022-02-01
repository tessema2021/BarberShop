using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberShop.Models;

namespace BarberShop.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        public TransactionRepository(IConfiguration config) : base(config) { }
        DateTime dateTime = DateTime.Now;


        public List<Transaction> GetAllTransactions()
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id,Comment,UserProfileId, TransactionDate,CustomerId
                         from Transaction ;
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Transaction> transactions = new List<Transaction>();
                    while (reader.Read())
                    {

                        Transaction transaction = new Transaction 
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),




                        };

                        transactions.Add(transaction);
                    }



                    return transactions;

                }
            }
        }
    }
}
