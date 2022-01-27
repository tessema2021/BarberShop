using BarberShop.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
       
        public CustomerRepository(IConfiguration config) : base(config) { }
       
        public List<Customer> GetAllCustomers()
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id,FirstName,LastName,UserProfileId,PhoneNumber,Email,Address
                         from Customer ;
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Customer> customers = new List<Customer>();
                    while (reader.Read())
                    {

                        Customer customer = new Customer
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            Email = reader.GetString(reader.GetOrdinal(" Email")),
                            Address = reader.GetString(reader.GetOrdinal("Address"))


                        };

                        customers.Add(customer);
                    }



                    return customers;

                }
            }
        }


    }
}
