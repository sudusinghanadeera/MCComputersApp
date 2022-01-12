namespace MCComputersApp.Areas.Admin.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Model;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Product Product { get; set; }

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
            Product = new Product();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            Product = await _context.Product.FirstOrDefaultAsync(u => u.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(Product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreateNew()
        {
            if (ModelState.IsValid)
            {
                Product.Name = SystemConstant.TextInfo.ToTitleCase(Product.Name).Trim();
                var doesProductExists = await this.IsExistsAsync(Product.Name);
                if (!doesProductExists)
                {
                    await _context.Product.AddAsync(Product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View("Create", Product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(u => u.Id == id);
            if (Product == null)
            {
                return NotFound();
            }

            return View(Product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit()
        {
            if (ModelState.IsValid)
            {
                Product.Name = SystemConstant.TextInfo.ToTitleCase(Product.Name).Trim();
                var categoryFromDb = await _context.Product.FirstOrDefaultAsync(u => u.Id == Product.Id);
                if (categoryFromDb == null)
                {
                    return NotFound();
                }

                if (categoryFromDb.Name != Product.Name)
                {
                    var doesProductExists = await this.IsExistsAsync(Product.Name);
                    if (doesProductExists)
                    {
                        return View("Edit", Product);
                    }
                }

                categoryFromDb.Name = Product.Name;
                categoryFromDb.Price = Product.Price;
                _context.Update(categoryFromDb);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Edit", Product);
        }

        #region API Calls

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.Product.OrderBy(c => c.Name).ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var filmFromDb = await _context.Product.FirstOrDefaultAsync(u => u.Id == id);
            if (filmFromDb == null)
            {
                return Json(new { success = false, message = SystemConstant.DeleteError });
            }

            _context.Product.Remove(filmFromDb);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = SystemConstant.DeleteError });
        }

        #endregion

        private async Task<bool> IsExistsAsync(string name)
        {
            var doesProductExists = await _context.Product.AnyAsync(s => s.Name == name);
            if (doesProductExists)
            {
                //Error
                TempData["StatusMessage"] = "Error: Product already exists. Please use another name.";
                return true;
            }

            return false;
        }
    }
}
