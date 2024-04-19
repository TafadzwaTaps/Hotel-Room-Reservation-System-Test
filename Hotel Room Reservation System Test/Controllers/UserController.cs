using Microsoft.AspNetCore.Mvc;
using Hotel_Room_Reservation_System_Test.Databases;
using Hotel_Room_Reservation_System_Test.Models;
using System.Linq;

namespace Hotel_Room_Reservation_System_Test.Controllers
{
    public class UserController : Controller
    {
        private readonly HotelDbContext _dbContext;

        public UserController(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var users = _dbContext.User.ToList();
            return View(users);
        }

        public ActionResult Details(int id)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.User.Add(user);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = user.UserId });
            }
            return View(user);
        }

        public ActionResult Edit(int id)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.User.Update(user);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                _dbContext.User.Remove(user);
                _dbContext.SaveChanges();
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
