using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fourth_MVC.Data;
using Fourth_MVC.Models;

namespace Fourth_MVC.Controllers
{
    public class EmployeeComplexesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeComplexesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Complexes.ToListAsync());
        }

        // ✅ Details
        public async Task<IActionResult> Details(int id)
        {
            var complex = await _context.Complexes.FindAsync(id);
            if (complex == null) return NotFound();
            return View(complex);
        }

        // ✅ Create
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Complex complex)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complex);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complex);
        }

        // ✅ Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var complex = await _context.Complexes.FindAsync(id);
            if (complex == null) return NotFound();
            return View(complex);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Complex complex)
        {
            if (id != complex.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(complex);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complex);
        }

        // ✅ Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var complex = await _context.Complexes.FindAsync(id);
            if (complex == null) return NotFound();
            return View(complex);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complex = await _context.Complexes.FindAsync(id);
            if (complex != null)
            {
                _context.Complexes.Remove(complex);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
