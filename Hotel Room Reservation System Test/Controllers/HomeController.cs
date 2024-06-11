using Hotel_Room_Reservation_System_Test.Databases;
using Hotel_Room_Reservation_System_Test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Hotel_Room_Reservation_System_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly HotelDbContext _dbContext;
        public HomeController(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
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

        [Authorize(Roles = "Admin")]
        public IActionResult TaskBoard()
        {
            var reservations = _dbContext.Reservation.ToList();
            return View(reservations);
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Inbox()
        {
            var reviews = _dbContext.Review.ToList();
            return View(reviews);
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Account()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _dbContext.User.FirstOrDefault(u => u.UserName == userName);
            return View(user);
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Settings()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _dbContext.User.FirstOrDefault(u => u.UserName == userName);
            return View(user);
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
