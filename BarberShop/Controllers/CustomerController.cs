using BarberShop.Models;
using BarberShop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BarberShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IServiceRepository _serviceRepo;
        private readonly ICustomerServiceRepository _customerServiceRepo;
        public CustomerController(ICustomerRepository customerRepository,IServiceRepository serviceRepository,ICustomerServiceRepository customerServiceRepository)
        {
            _customerRepo = customerRepository;
            _serviceRepo = serviceRepository;
            _customerServiceRepo = customerServiceRepository;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            var customers = _customerRepo.GetAllCustomers();
            return View(customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
           Customer customer = _customerRepo.GetCustomerById(id);
            
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
               
                int userProfileId = GetCurrentUserId();
                customer.UserProfileId = userProfileId;
                customer.CreateDateTime = DateTime.Now;
                _customerRepo.AddCustomer(customer);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(customer);
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            int userProfileId = GetCurrentUserId();
            Customer customer = _customerRepo.GetById(id);


            if (customer.UserProfileId == userProfileId)
            {
                return View(customer);
            }

            return Unauthorized();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer customer)
        {
            int userProfileId = GetCurrentUserId();
            customer.CreateDateTime = DateTime.Now;
            Customer Exstingcustomer = _customerRepo.GetById(id);


            if (Exstingcustomer.UserProfileId == userProfileId)
            {

                try
                {
                    _customerRepo.UpdateCustomer(customer);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(customer);
                }
            }

            return Unauthorized();
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            int userProfileId = GetCurrentUserId();
            Customer customer = _customerRepo.GetById(id);


            if (customer.UserProfileId == userProfileId)
            {
               
                return View(customer);
            }

            return Unauthorized();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Customer customer)
        {
            int userProfileId = GetCurrentUserId();
            Customer Exstingcustomer = _customerRepo.GetById(id);

            if (Exstingcustomer.UserProfileId == userProfileId)
            {

                try
                {
                    _customerRepo.DeleteCustomer(id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(customer);
                }
            }

            return Unauthorized();
        }


        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }


    }
}
