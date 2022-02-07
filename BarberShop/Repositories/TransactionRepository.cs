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
                       SELECT t.Id, t.Comment, t.UserProfileId, t.TransactionDate, t.CustomerId, 
                              c.Id, c.FirstName, c.LastName, c.UserProfileId, c.CreateDateTime, c.PhoneNumber,c.Email As Customeremail,Address,
                              up.Id, up.FirebaseId, up.FirstName, up.LastName, up.CreateDateTime, up.DisplayName, up.Email, up.UserTypeId
                         from [Transaction] t
                            left join Customer c on t.CustomerId = c.Id
                            left join UserProfile up on t.UserProfileId = up.Id
                        
                    ";
                  
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Transaction> transactions = new List<Transaction>();
                    while (reader.Read())
                    {

                        Transaction transaction = new Transaction 
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment")),
                            TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                            Customer  = new Customer()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("createDateTime")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address"))
                            },
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            UserProfile = new UserProfile
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
                                UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            }

                    };

                        transactions.Add(transaction);
                    }



                    return transactions;

                }
            }
        }
       public void CreateTransactionService(int serviceId , int transactionId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         INSERT INTO TransactionService (TransactionId,ServiceId)
                             Values(@transactionId,@serviceId)    
      ";
                    cmd.Parameters.AddWithValue("@transactionId", transactionId);
                    cmd.Parameters.AddWithValue("@serviceId", serviceId);
                    cmd.ExecuteNonQuery();
                }
                
            }
        }

        public Transaction GetById(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, Comment, UserProfileId, TransactionDate, CustomerId
                                    FROM [Transaction]  
                                    WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", Id);

                    Transaction transaction = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        transaction = new Transaction
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        };
                    }
                    reader.Close();

                    return transaction;
                }
            }
        }

       /* public Transaction GetTransactionById(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                   SELECT t.Id, t.Comment, t.UserProfileId, t.TransactionDate, t.CustomerId,
                                    ts.ServiceId
                                    FROM [Transaction] t  
                                    Left join TransactionService ts on ts.TransactionId = t.Id
                                    WHERE t.Id = @Id";

                    cmd.Parameters.AddWithValue("@id", Id);

                    Transaction transaction = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        transaction = new Transaction
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                            ServiceId = reader.GetInt32(reader.GetOrdinal("ServiceId")),
                            Service = new Service
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Cost = reader.GetInt32(reader.GetOrdinal("Cost")),

                            }


                        };
                    }
                    reader.Close();

                    return transaction;
                }
            }
        }*/



        public List<Transaction> GetByCustomerId(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, Comment, UserProfileId, TransactionDate, CustomerId
                                    FROM [Transaction]  
                                    WHERE CustomerId = @id";

                    cmd.Parameters.AddWithValue("@id", Id);

                    List<Transaction> transactions = new List<Transaction>();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Transaction transaction = new Transaction
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment")),
                            TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                        
                        };



                        transactions.Add(transaction);
                    }

                    return transactions;
                }
            }
        }


        public void AddTransaction(Transaction transaction)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                               INSERT INTO [Transaction] (Comment,UserProfileId, TransactionDate,CustomerId)
                               OUTPUT INSERTED.ID
                               VALUES (@comment,@userProfileId,SYSDateTime(),@customerId)
                                ";

                    cmd.Parameters.AddWithValue("@comment", transaction.Comment);
                    cmd.Parameters.AddWithValue("@userProfileId", transaction.UserProfileId);
                    cmd.Parameters.AddWithValue("@customerId", transaction.CustomerId);

                    transaction.Id = (int)cmd.ExecuteScalar();

                    

                }
            }
        }

        public void UpdateTransaction(Transaction transaction)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [Transaction]
                            SET 
                                Comment = @comment 
                             
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@comment", transaction.Comment);
                    cmd.Parameters.AddWithValue("@id", transaction.Id);

                    // becuse we don't need any data back from database
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void DeleteTransaction(int transactionId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                //in a delete we filter by id to make sure deleting only one 
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM [Transaction]
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", transactionId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
