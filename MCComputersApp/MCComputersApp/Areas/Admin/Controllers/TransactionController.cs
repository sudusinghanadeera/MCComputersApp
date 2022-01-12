namespace MCComputersApp.Areas.Admin.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public TransactionViewModel TransactionViewModel { get; set; }

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
            TransactionViewModel = new TransactionViewModel()
            {
                Transaction = new Transaction(),
                TransactionItemList = new List<TransactionItem>()
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            TransactionViewModel.Transaction = await _context.Transaction.FirstOrDefaultAsync(m => m.Id == id);
            TransactionViewModel.TransactionItemList = await _context.TransactionItem.Where(m => m.TransactionId == id).ToListAsync();

            if (TransactionViewModel.Transaction == null)
            {
                return NotFound();
            }
            return View(TransactionViewModel);
        }

        #region API Calls

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new
            {
                data = await _context.Transaction.OrderByDescending(c => c.Id).ToListAsync()
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Json(new { success = false, message = SystemConstant.DeleteError });
        }

        #endregion



        private async Task LoadDetailsAsync(bool isCustomError = false)
        {
            TransactionViewModel.Transaction = new Transaction();
            TransactionViewModel.TransactionItemList = new List<TransactionItem>();
            TransactionViewModel.StatusMessage = isCustomError ? StatusMessage : SystemConstant.RequestError;
        }

    }
}