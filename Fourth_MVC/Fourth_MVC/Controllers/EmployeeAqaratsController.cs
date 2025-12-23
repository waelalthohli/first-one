using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fourth_MVC.Data;
using Fourth_MVC.Models;

namespace Fourth_MVC.Controllers
{
    public class EmployeeAqaratsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeAqaratsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aqarats.ToListAsync());
        }

        // ✅ Details
        public async Task<IActionResult> Details(int id)
        {
            var aqarat = await _context.Aqarats.FindAsync(id);
            if (aqarat == null) return NotFound();
            return View(aqarat);
        }

        // ✅ Create
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aqarat aqarat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aqarat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aqarat);
        }

        // ✅ Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var aqarat = await _context.Aqarats.FindAsync(id);
            if (aqarat == null) return NotFound();
            return View(aqarat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aqarat aqarat)
        {
            if (id != aqarat.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(aqarat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aqarat);
        }

        // ✅ Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var aqarat = await _context.Aqarats.FindAsync(id);
            if (aqarat == null) return NotFound();
            return View(aqarat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aqarat = await _context.Aqarats.FindAsync(id);
            if (aqarat != null)
            {
                _context.Aqarats.Remove(aqarat);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
