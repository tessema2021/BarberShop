using BarberShop.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Repositories
{
    public class ServiceRepository : BaseRepository, IServiceRepository
    {

        public ServiceRepository(IConfiguration config) : base(config) { }
        DateTime dateTime = DateTime.Now;

        public List<Service> GetAllServices()
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id,[Name],Cost
                         from Service ;
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Service> services = new List<Service>();
                    while (reader.Read())
                    {

                        Service service = new Service
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Cost = reader.GetInt32(reader.GetOrdinal("Cost")),



                        };

                        services.Add(service);
                    }



                    return services;

                }
            }
        }
    }
}
