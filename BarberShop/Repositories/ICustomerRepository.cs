using BarberShop.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace BarberShop.Repositories
{
    public interface ICustomerRepository
    {

        List<Customer> GetAllCustomers();
        void AddCustomer(Customer customer);
    }
}