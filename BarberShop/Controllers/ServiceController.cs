using BarberShop.Models;
using BarberShop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepo;
        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepo = serviceRepository;
        }


        // GET: ServiceController
        public ActionResult Index()
        {
            var services = _serviceRepo.GetAllServices();
            return View(services);
        }

        // GET: ServiceController/Details/5
        public ActionResult Details(int id)
        {
            Service service = _serviceRepo.GetServiceById(id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);

        }
       

        // GET: ServiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service)
        {
            try
            {

               
                _serviceRepo.AddService(service);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(service);
            }
        }

        // GET: ServiceController/Edit/5
        public ActionResult Edit(int id)
        {
           
            Service service = _serviceRepo.GetServiceById(id);

                return View(service);
            
        }

        // POST: ServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Service service)
        {
            try
            {
                _serviceRepo.UpdateService(service);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(service);
            }
        }

        // GET: ServiceController/Delete/5
        public ActionResult Delete(int id)
        {
            Service service = _serviceRepo.GetServiceById(id);
            if (service == null)
            {
                return StatusCode(404);
            }
            return View(service);

        }

        // POST: ServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Service service)
        {
            try
            {
                _serviceRepo.DeleteService(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
