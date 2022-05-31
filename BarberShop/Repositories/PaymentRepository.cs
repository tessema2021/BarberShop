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

        public List<Payment> GetAllPayments()
        {

            using (var conn = Connection)
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                        SELECT Id,PaymentType, CustomerId,UserProfileId, CreateDateTime,ServiceId
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
                        CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        ServiceId = reader.GetInt32(reader.GetOrdinal("ServiceId")),
                        UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                        CreateDateTime = reader.GetDateTime(reader.GetOrdinal("createDateTime")),




                    };

                    payments.Add(payment);
                }



                return payments;
            }
        }

        public Payment GetPaymentById(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                     SELECT Id,PaymentType, CustomerId,UserProfileId, CreateDateTime,ServiceId
                                      from Payment
                                    WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", Id);

                    Payment payment = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        payment = new Payment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PaymentType = reader.GetString(reader.GetOrdinal("PaymentType")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                            ServiceId = reader.GetInt32(reader.GetOrdinal("ServiceId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("createDateTime")),


                        };
                    }
                    reader.Close();

                    return payment;
                }
            }
        }


        public void AddPayment(Payment payment)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                  INSERT INTO Payment (PaymentType, CustomerId,UserProfileId, CreateDateTime,ServiceId)
                                  OUTPUT INSERTED.ID
                                  VALUES (@paymentType, @customerId,@userProfileId, @createDateTime,@serviceId);
                                   ";

                    cmd.Parameters.AddWithValue("@paymentType", payment.PaymentType);
                    cmd.Parameters.AddWithValue("@customerId", payment.CustomerId);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();

                    payment.Id = newlyCreatedId;

                }
            }
        }


    }
}
