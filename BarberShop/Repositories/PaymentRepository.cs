using BarberShop.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Repositories
{
    
    public class PaymentRepository : BaseRepository
    {
        public PaymentRepository(IConfiguration config) : base(config) { }

        private DateTime dateTime = DateTime.Now;

        public List<Payment> GetAllPaymentss()
        {

            using (var conn = Connection)
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                        SELECT Id,PaymentType, Cost,UserProfileId, CreateDateTime,ServiceId
                         from Payment ;
                    ";

                SqlDataReader reader = cmd.ExecuteReader();

                List<Payment> payments = new List<Payment>();
                while (reader.Read())
                {

                    Payment payment = new Payment
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        PaymentType = reader.GetString(reader.GetOrdinal("PaymentType")),
                        Cost = reader.GetInt32(reader.GetOrdinal("Cost")),
                        ServiceId = reader.GetInt32(reader.GetOrdinal("ServiceId")),
                        UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                        CreateDateTime = reader.GetDateTime(reader.GetOrdinal("createDateTime")),




                    };

                    payments.Add(payment);
                }



                return payments;
            }
        }
    }
}
