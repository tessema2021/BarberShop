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

        public void AddCustomerService(CustomerServiceRepository customerService)
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
                    cmd.Parameters.AddWithValue("@customerId", customerService.customerId);

                    customerService.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public PostTag GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT PostId, TagID
                         FROM PostTag
                        WHERE Id = @id;";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    PostTag postTag = new PostTag();

                    if (reader.Read())
                    {
                        postTag.Id = id;
                        postTag.PostId = reader.GetInt32(reader.GetOrdinal("PostId"));
                        postTag.TagId = reader.GetInt32(reader.GetOrdinal("TagId"));
                    }

                    reader.Close();

                    return postTag;
                }
            }
        }




    }
}
