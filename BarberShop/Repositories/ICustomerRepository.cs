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
         Customer GetById(int Id);
        Customer GetCustomerById(int Id);
        void DeleteCustomer(int customerId);

       
    }
}