using BarberShop.Models;
using BarberShop.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Repositories
{
    public class TransactionServiceRepository : BaseRepository, ITransactionServiceRepository
    {
        public TransactionServiceRepository(IConfiguration config) : base(config) { }

        public List<TransactionService> GetByTransactionId(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                   select t.Id,ts.TransactionId,ts.ServiceId,s.Id,s.[Name],s.Cost
                                            from [TransactionService] ts
                                            left join [Service] s on s.Id = ts.ServiceId
                                            left join [Transaction] t on t.Id = ts.TransactionId
                                            Where t.Id = @id";

                    cmd.Parameters.AddWithValue("@id", Id);

                    List<TransactionService> transactionServices = new List<TransactionService>();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        TransactionService transactionService = new TransactionService
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            TransactionId = reader.GetInt32(reader.GetOrdinal("TransactionId")),
                            ServiceId = reader.GetInt32(reader.GetOrdinal("ServiceId")),
                            Service = new Service()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Cost = DbUtils.GetInt(reader, "Cost"),

                            },

                        };

                        transactionServices.Add(transactionService);
                    }

                    return transactionServices;
                }
            }
        }
        public void DeleteTransactionServices(int transactionId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                //in a delete we filter by id to make sure deleting only one 
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM [TransactionService]
                            WHERE TransactionId = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", transactionId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
