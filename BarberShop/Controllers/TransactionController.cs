using BarberShop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BarberShop.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepo;
        public TransactionController(ITransactionRepository TransactionRepository )
        {
            _transactionRepo = TransactionRepository;
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

        // GET: TransactionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            try
            {
                int userProfileId = GetCurrentUserId();
                transaction.UserProfileId = userProfileId;
                _transactionRepo.AddTransaction(transaction);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(transaction);
            }
        }

        // GET: TransactionController/Edit/5
        public ActionResult Edit(int id)
        {
            Transaction transaction = _transactionRepo.GetById(id);
            return View();
        }

        // POST: TransactionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Transaction transaction)
        {
            try
            {
                _transactionRepo.UpdateTransaction(transaction);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionController/Delete/5
        public ActionResult Delete(int id)
        {
            Transaction transaction = _transactionRepo.GetById(id);
            if (transaction == null)
            {
                return StatusCode(404);
            }
            return View(transaction);
        }

        // POST: TransactionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Transaction transaction)
        {
            try
            {
                _transactionRepo.DeleteTransaction(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
