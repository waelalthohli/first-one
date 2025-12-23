using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fourth_MVC.Data;
using Fourth_MVC.Models;

namespace Fourth_MVC.Controllers
{
    public class EmployeeDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard
        public IActionResult Dashboard()
        {
            return View(new SearchViewModel());
        }

        // POST: Search
        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            var vm = new SearchViewModel
            {
                Query = query,
                Aqarats = await _context.Aqarats
                    .Where(a => a.Name.Contains(query) || a.Description.Contains(query))
                    .ToListAsync(),

                Lands = await _context.Lands
                    .Where(l => l.Name.Contains(query) || l.Description.Contains(query))
                    .ToListAsync(),

                Complexes = await _context.Complexes
                    .Where(c => c.Name.Contains(query) || c.Description.Contains(query))
                    .ToListAsync(),

                Fields = await _context.Fields
                    .Where(f => f.Name.Contains(query) || f.Description.Contains(query))
                    .ToListAsync()
            };

            return View("Index", vm);
        }
    }
}
