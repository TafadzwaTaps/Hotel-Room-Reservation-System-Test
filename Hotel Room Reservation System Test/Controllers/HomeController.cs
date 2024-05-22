using Hotel_Room_Reservation_System_Test.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotel_Room_Reservation_System_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Homepage()
        {
            var role = "Admin"; // Or retrieve the actual role from your authentication system

            ViewData["Role"] = role;

            return View();
        }

        public IActionResult Account()
        {
            // Add logic for Account view
            return View();
        }

        public IActionResult Inbox()
        {
            // Add logic for Inbox view
            return View();
        }

        public IActionResult TaskBoard()
        {
            // Add logic for TaskBoard view
            return View();
        }

        public IActionResult Settings()
        {
            // Add logic for Settings view
            return View();
        }

        public IActionResult Logout()
        {
            // Clear session data
            HttpContext.Session.Clear();

            // Manually remove the authentication cookies
            Response.Cookies.Delete(".AspNetCore.Cookies");
            Response.Cookies.Delete("YourCustomCookieName"); // If you have a custom cookie name

            // Redirect to the login page
            return RedirectToAction("Login", "Account");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
