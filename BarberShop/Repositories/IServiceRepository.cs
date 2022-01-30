using BarberShop.Models;
using System.Collections.Generic;

namespace BarberShop.Repositories
{
    public interface IServiceRepository
    {
        List<Service> GetAllServices();
        Service GetServiceById(int Id);
        void AddService(Service service);
        void UpdateService(Service service);
        void DeleteService(int serviceId);
    }
}