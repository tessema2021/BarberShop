using BarberShop.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Repositories
{
    public class CustomerServiceRepository : BaseRepository, ICustomerServiceRepository
    {
        public CustomerServiceRepository(IConfiguration config) : base(config) { }

        public void AddCustomerService(CustomerService customerService)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO CustomerService(ServiceId, CustomerId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@serviceId, @customerId)";

                    cmd.Parameters.AddWithValue("@serviceId", customerService.ServiceId);
                    cmd.Parameters.AddWithValue("@customerId", customerService.CustomerId);

                    customerService.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public CustomerService GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT CustomerId, ServiceID
                         FROM CustomerService
                        WHERE Id = @id;";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    CustomerService customerService = new CustomerService();

                    if (reader.Read())
                    {
                        customerService.Id = id;
                        customerService.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomertId"));
                        customerService.ServiceId = reader.GetInt32(reader.GetOrdinal("ServiceId"));
                    }

                    reader.Close();

                    return customerService;
                }
            }
        }

       



    }
}
   
