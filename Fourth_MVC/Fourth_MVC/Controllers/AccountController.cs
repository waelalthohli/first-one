using Fourth_MVC.Data;
using Fourth_MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Fourth_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // بدون تشفير
            EmployeeRegister? user = await _context.Employees
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                // التوجيه حسب الدور
                if (user.Role == "Employee")
                    return RedirectToAction("Dashboard", "Home");
                else
                    return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "❌ البريد الإلكتروني أو كلمة المرور غير صحيحة";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // تسجيل مستثمر جديد
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(EmployeeRegister model)
        {
            if (ModelState.IsValid)
            {
                // تحقق من وجود البريد مسبقاً
                if (await _context.Employees.AnyAsync(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "❌ البريد الإلكتروني مستخدم من قبل");
                    return View(model);
                }

                // أي مستخدم جديد = مستثمر
                model.Role = "Investor";

                _context.Employees.Add(model);      // إضافة إلى DbContext
                await _context.SaveChangesAsync();   // حفظ التغييرات في القاعدة

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

    }
}
