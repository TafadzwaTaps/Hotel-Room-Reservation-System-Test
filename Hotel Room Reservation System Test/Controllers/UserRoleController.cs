using Microsoft.AspNetCore.Mvc;
using Sample_Hotel_Room_Reservation_System.Databases;
using Sample_Hotel_Room_Reservation_System.Models;
using System.Linq;

namespace Sample_Hotel_Room_Reservation_System.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly HotelDbContext _dbContext;

        public UserRoleController(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var userRoles = _dbContext.UserRole.ToList();
            return View(userRoles);
        }

        public ActionResult Details(string roleId)
        {
            var userRole = _dbContext.UserRole.FirstOrDefault(ur => ur.RoleId == roleId);
            if (userRole == null)
            {
                return NotFound();
            }
            return View(userRole);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                _dbContext.UserRole.Add(userRole);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Details), new { roleId = userRole.RoleId });
            }
            return View(userRole);
        }

        public ActionResult Edit(string roleId)
        {
            var userRole = _dbContext.UserRole.FirstOrDefault(ur => ur.RoleId == roleId);
            if (userRole == null)
            {
                return NotFound();
            }
            return View(userRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string roleId, UserRole userRole)
        {
            if (roleId != userRole.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.UserRole.Update(userRole);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(userRole);
        }

        public ActionResult Delete(string roleId)
        {
            var userRole = _dbContext.UserRole.FirstOrDefault(ur => ur.RoleId == roleId);
            if (userRole == null)
            {
                return NotFound();
            }
            return View(userRole);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string roleId)
        {
            var userRole = _dbContext.UserRole.FirstOrDefault(ur => ur.RoleId == roleId);
            _dbContext.UserRole.Remove(userRole);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
