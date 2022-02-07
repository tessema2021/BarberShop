using BarberShop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Controllers
{
    public class TransactionServiceController : Controller
    {
        private readonly ITransactionServiceRepository _transactionServiceRepo;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IServiceRepository _serviceRepository;
        public TransactionServiceController(ITransactionServiceRepository transactionServiceRepo,ITransactionRepository transactionRepository,
                                             IServiceRepository serviceRepository)
        {
            _transactionServiceRepo = transactionServiceRepo;
            _transactionRepository = transactionRepository;
            _serviceRepository = serviceRepository;
        }
        // GET: TransactionServiceController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TransactionServiceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //GET
        public ActionResult ServicesByTransaction(int id)
        {
            var transactionService = _transactionServiceRepo.GetByTransactionId(id);
            ViewData["TransactionId"] = id;


            if (transactionService == null)
            {
                return NotFound();
            }

            return View(transactionService);
        }


        // GET: TransactionServiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionServiceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionServiceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
