using Microsoft.AspNetCore.Mvc;

namespace WebFormL1.Controllers
{
    public class AuthenticateController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string mail, string pass)
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string email = config["Authentication:Email"];
            string password = config["Authentication:Password"];
            if (mail == email && pass == password)
            {
                HttpContext.Session.SetString("IsAuthenticated", "true");
                return RedirectToAction("Index", "Employees");
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred when login.";
                return View();
            }
        }
    }
}
