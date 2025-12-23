using Fourth_MVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fourth_MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Employee")] // ✅ السماح فقط للموظفين
        public IActionResult Dashboard()
        {
            return View();
        }
        // ✅ Search Action
        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return View("Dashboard");

            // البحث في الجداول الأربعة
            var aqarats = await _context.Aqarats
                .Where(c =>
                      c.Name.Contains(query) ||
                      c.Description.Contains(query) ||
                      c.Location.Contains(query) ||
                      c.Area.Contains(query)
              ).ToListAsync();

            var lands = await _context.Lands
                .Where(c =>
                      c.Name.Contains(query) ||
                      c.Description.Contains(query) ||
                      c.Location.Contains(query) ||
                      c.Area.Contains(query)
              ).ToListAsync();

            var complexes = await _context.Complexes
               .Where(c =>
                      c.Name.Contains(query) ||
                      c.Description.Contains(query) ||
                      c.Location.Contains(query) ||
                      c.Capacity.Contains(query)
              ).ToListAsync();


            var fields = await _context.Fields
                .Where(c =>
                      c.Name.Contains(query) ||
                      c.Description.Contains(query) ||
                      c.Location.Contains(query) ||
                      c.Area.Contains(query)
              ).ToListAsync();

            // نرسل النتائج للـ View
            ViewBag.Query = query;
            ViewBag.Aqarats = aqarats;
            ViewBag.Lands = lands;
            ViewBag.Complexes = complexes;
            ViewBag.Fields = fields;

            return View("Dashboard");
        }
    }
}
