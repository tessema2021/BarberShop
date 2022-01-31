using BarberShop.Models;
using System.Collections.Generic;

namespace BarberShop.Repositories
{
    public interface ICustomerServiceRepository
    {
        void AddCustomerService(CustomerService customerService);
        /*List<CustomerService> GetAllCUstomerServicesByCustomerId(int id);*/
        CustomerService GetById(int id);
    }
}