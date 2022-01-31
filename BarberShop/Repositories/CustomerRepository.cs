﻿using BarberShop.Models;
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
        DateTime dateTime = DateTime.Now;
        public List<Customer> GetAllCustomers()
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id,FirstName,LastName,UserProfileId, CreateDateTime,PhoneNumber,Email,Address
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
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("createDateTime")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Address = reader.GetString(reader.GetOrdinal("Address"))


                        };

                        customers.Add(customer);
                    }



                    return customers;

                }
            }
        }


        public void AddCustomer(Customer customer)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                               INSERT INTO Customer (FirstName,LastName,UserProfileId, CreateDateTime,PhoneNumber,Email,Address)
                               OUTPUT INSERTED.ID
                               VALUES (@firstName, @lastName, @userProfileId,@createDateTime, @PhoneNumber, @email,@address);
                                ";

                    cmd.Parameters.AddWithValue("@firstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@userProfileId", customer.UserProfileId);
                    cmd.Parameters.AddWithValue("@createDateTime", customer.CreateDateTime);
                    cmd.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", customer.Email);
                    cmd.Parameters.AddWithValue("@address", customer.Address);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();

                    customer.Id = newlyCreatedId;

                }
            }
        }


        public List<Customer> GetAllCustomerServicesByCustomerId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                           SELECT Customer.Id, Customer.FirstName,Customer.FirstName, Service.Name as 'Service Name',Service.Cost as 'Service Cost'
                            FROM CustomerService
                            LEFT JOIN Customer ON CustomerService.CustomerId = Customer.Id
                            LEFT JOIN Service on CustomerService.ServiceId = Service.Id
                            WHERE Customer.Id = @customerId";

                    cmd.Parameters.AddWithValue("@customerId", id);

                    var customers = new List<Customer>();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Customer customer = new Customer()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("Firstname")),
                            LastName = reader.GetString(reader.GetOrdinal("Lastname")),
                          /*  Service = new Service
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ServiceId")),
                                Name = reader.GetString(reader.GetOrdinal("serviceName"))
                            }*/
                        };
                        customers.Add(customer);
                    }
                    reader.Close();
                    return customers;
                }
            }
        }




        public Customer GetCustomerById(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id,FirstName, LastName,UserProfileId, CreateDateTime,PhoneNumber, Email, Address
                                    FROM Customer
                                    WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", Id);

                    Customer customer = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        customer = new Customer
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("createDateTime")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            
                        };
                    }
                    reader.Close();

                    return customer;
                }
            }
        }






        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Customer
                            SET 
                                FirstName = @firstName, 
                                LastName = @lastName, 
                                UserProfileId = @userProfileId,
                                CreateDateTime = @createDateTime,
                                PhoneNumber = @phoneNumber, 
                                Email = @email, 
                                Address = @address
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@firstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@userProfileId", customer.UserProfileId);
                    cmd.Parameters.AddWithValue("@createDateTime", customer.CreateDateTime);
                    cmd.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", customer.Email);
                    cmd.Parameters.AddWithValue("@address", customer.Address);
                    cmd.Parameters.AddWithValue("@id", customer.Id);

                    // becuse we don't need any data back from database
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                //in a delete we filter by id to make sure deleting only one 
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Customer
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", customerId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
