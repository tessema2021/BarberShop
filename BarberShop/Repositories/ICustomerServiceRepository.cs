using BarberShop.Models;
using System.Collections.Generic;

namespace BarberShop.Repositories
{
    public interface ICustomerServiceRepository
    {
        void AddCustomerService(CustomerServiceRepository customerService);
        List<CustomerService> GetAllCustomerServicessByCustomerId(int id);


    }
}