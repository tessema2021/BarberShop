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


        public Service GetServiceById(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id,[Name], Cost
                                    FROM Service
                                    WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", Id);

                    Service service = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        service = new Service
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Cost = reader.GetInt32(reader.GetOrdinal("Cost")),


                        };
                    }
                    reader.Close();

                    return service;
                }
            }
        }


        public void AddService(Service service)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                               INSERT INTO Service (Name,Cost)
                               OUTPUT INSERTED.ID
                               VALUES (@name, @cost);
                                ";

                    cmd.Parameters.AddWithValue("@name", service.Name);
                    cmd.Parameters.AddWithValue("@cost", service.Cost);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();

                    service.Id = newlyCreatedId;

                }
            }
        }


        public void UpdateService(Service service)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Service
                            SET 
                                Name = @name, 
                                Cost = @cost
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@name", service.Name);
                    cmd.Parameters.AddWithValue("@cost", service.Cost);
                    cmd.Parameters.AddWithValue("@id", service.Id);

                    // becuse we don't need any data back from database
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void DeleteService(int serviceId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                //in a delete we filter by id to make sure deleting only one 
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Service
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", serviceId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
