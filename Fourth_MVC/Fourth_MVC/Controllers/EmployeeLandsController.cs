using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fourth_MVC.Data;
using Fourth_MVC.Models;

namespace Fourth_MVC.Controllers
{
    public class EmployeeLandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeLandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lands.ToListAsync());
        }

        // ✅ Details
        public async Task<IActionResult> Details(int id)
        {
            var land = await _context.Lands.FindAsync(id);
            if (land == null) return NotFound();
            return View(land);
        }

        // ✅ Create
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Land land)
        {
            if (ModelState.IsValid)
            {
                _context.Add(land);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(land);
        }

        // ✅ Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var land = await _context.Lands.FindAsync(id);
            if (land == null) return NotFound();
            return View(land);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Land land)
        {
            if (id != land.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(land);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(land);
        }

        // ✅ Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var land = await _context.Lands.FindAsync(id);
            if (land == null) return NotFound();
            return View(land);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var land = await _context.Lands.FindAsync(id);
            if (land != null)
            {
                _context.Lands.Remove(land);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
