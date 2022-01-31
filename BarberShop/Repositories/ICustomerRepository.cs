using BarberShop.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace BarberShop.Repositories
{
    public interface ICustomerRepository
    {

        List<Customer> GetAllCustomers();
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        Customer GetCustomerById(int Id);
        void DeleteCustomer(int customerId);
        List<Customer> GetAllCustomerServicesByCustomerId(int id);
    }
}