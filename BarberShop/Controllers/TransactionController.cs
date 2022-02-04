using BarberShop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BarberShop.Models.ViewModels;

namespace BarberShop.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly ICustomerRepository _customerRepository;
        private readonly IServiceRepository _serviceRepository;
        public TransactionController(ITransactionRepository transactionRepository, ICustomerRepository customerRepository, IServiceRepository serviceRepository)
        {
            _transactionRepo = transactionRepository;
            _customerRepository = customerRepository;
            _serviceRepository = serviceRepository;
        }

        // GET: TransactionController
        public ActionResult Index()
        {
            var transactions = _transactionRepo.GetAllTransactions();
            return View(transactions);
        }

        // GET: TransactionController/Details/5
        public ActionResult Details(int id)
        {
            Transaction  transaction = _transactionRepo.GetById(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }
        //GET
        public ActionResult CustomerTransactions(int id)
        {
            var transaction = _transactionRepo.GetByCustomerId(id);
            ViewData["CustomerId"] = id;


            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: TransactionController/Create
        public ActionResult Create()
        {
            var vm = new TransactionFormViewModel();
            List<Customer> customers = _customerRepository.GetAllCustomers();
            List<Service> services = _serviceRepository.GetAllServices();
            vm.Services = services;
            vm.Customers = customers;
            return View(vm);
        }

        // POST: TransactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionFormViewModel vm)
        {
            try
            {
                int userProfileId = GetCurrentUserId();
                vm.Transaction.UserProfileId = userProfileId;
                _transactionRepo.AddTransaction(vm.Transaction);
                foreach(var serviceId in vm.SelectedServiceIds)
                {
                    _transactionRepo.CreateTransactionService(serviceId,vm.Transaction.Id);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                List<Customer> customers = _customerRepository.GetAllCustomers();
                List<Service> services = _serviceRepository.GetAllServices();
                vm.Services = services;
                vm.Customers = customers;
                return View(vm);
            }
        }

        // GET: TransactionController/Edit/5
        public ActionResult Edit(int id)
        {
            int userProfileId = GetCurrentUserId();
            Transaction transaction = _transactionRepo.GetById(id);



            if (transaction.UserProfileId == userProfileId)
            {
                return View(transaction);
            }

            return Unauthorized();
        }

        // POST: TransactionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Transaction transaction)
        {
            int userProfileId = GetCurrentUserId();
            transaction.TransactionDate = DateTime.Now;
            Transaction Exstingtransaction = _transactionRepo.GetById(id);



            if (Exstingtransaction.UserProfileId == userProfileId)
            {

                try
                {
                    _transactionRepo.UpdateTransaction(transaction);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(transaction);
                }
            }

            return Unauthorized();
        }

        // GET: TransactionController/Delete/5
        public ActionResult Delete(int id)
        {
            int userProfileId = GetCurrentUserId();
            Transaction transaction = _transactionRepo.GetById(id);
            if (transaction.UserProfileId == userProfileId)
            {
                return View(transaction);
            }
            return Unauthorized();
        }

        // POST: TransactionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Transaction transaction)
        {
            int userProfileId = GetCurrentUserId();
            Transaction Exstingtransaction = _transactionRepo.GetById(id);
            if (Exstingtransaction.UserProfileId == userProfileId)
            {
                try
                {
                    _transactionRepo.DeleteTransaction(id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(transaction);
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
