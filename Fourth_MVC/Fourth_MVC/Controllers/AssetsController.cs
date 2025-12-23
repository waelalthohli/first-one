using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fourth_MVC.Data;
using Fourth_MVC.Models;

namespace Fourth_MVC.Controllers
{
    public class AssetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Assets/Lands
        public async Task<IActionResult> Lands()
        {
            var lands = await _context.Lands.ToListAsync(); // هنا جلب البيانات من جدول Lands
            return View(lands);
        }

        // GET: /Assets/Aqarat
        public async Task<IActionResult> Aqarat()
        {
            var aqarat = await _context.Aqarats.ToListAsync();
            return View(aqarat);
        }

        // GET: /Assets/Complexes
        public async Task<IActionResult> Complexs()
        {
            var complexes = await _context.Complexes.ToListAsync();
            return View(complexes);
        }

        // GET: /Assets/Fields
        public async Task<IActionResult> Fields()
        {
            var fields = await _context.Fields.ToListAsync();
            return View(fields);
        }
    }
}
