using Microsoft.AspNetCore.Mvc;
using Hotel_Room_Reservation_System_Test.Databases;
using Hotel_Room_Reservation_System_Test.Models;
using System.Linq;

namespace Hotel_Room_Reservation_System_Test.Controllers
{
    
    public class RoomController : Controller
    {
        private readonly HotelDbContext _dbContext;

        public RoomController(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var rooms = _dbContext.Room.ToList();
            return View(rooms);
        }

        public ActionResult Details(int id)
        {
            var room = _dbContext.Room.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {

                _dbContext.Room.Add(room);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = room.Id });
            }
            return View(room);
        }

        public ActionResult Edit(int id)
        {
            var room = _dbContext.Room.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Room.Update(room);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        public ActionResult Cancel(int id)
        {
            var room = _dbContext.Room.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var room = _dbContext.Room.FirstOrDefault(r => r.Id == id);
            if (room != null)
            {
                _dbContext.Room.Remove(room);
                _dbContext.SaveChanges();
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
