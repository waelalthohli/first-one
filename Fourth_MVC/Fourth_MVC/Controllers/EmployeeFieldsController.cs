using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fourth_MVC.Data;
using Fourth_MVC.Models;

namespace Fourth_MVC.Controllers
{
    public class EmployeeFieldsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeFieldsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fields.ToListAsync());
        }

        // ✅ Details
        public async Task<IActionResult> Details(int id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null) return NotFound();
            return View(field);
        }

        // ✅ Create
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Field field)
        {
            if (ModelState.IsValid)
            {
                _context.Add(field);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(field);
        }

        // ✅ Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null) return NotFound();
            return View(field);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Field field)
        {
            if (id != field.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(field);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(field);
        }

        // ✅ Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null) return NotFound();
            return View(field);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field != null)
            {
                _context.Fields.Remove(field);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
