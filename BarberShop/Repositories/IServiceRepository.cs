using BarberShop.Models;
using System.Collections.Generic;

namespace BarberShop.Repositories
{
    public interface IServiceRepository
    {
        List<Service> GetAllServices();
    }
}