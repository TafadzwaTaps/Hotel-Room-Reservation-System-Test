using Microsoft.AspNetCore.Mvc;
using Hotel_Room_Reservation_System_Test.Databases;
using Hotel_Room_Reservation_System_Test.Models;
using System.Linq;

namespace Hotel_Room_Reservation_System_Test.Controllers
{
    
    public class RoomController : Controller
    {
        // private readonly HotelDbContext _dbContext;
        private readonly Roomservice _roomservice;

        // List<Room> rooms1 = new List<Room>();

        public RoomController(Roomservice roomservice)
        {
            _roomservice = roomservice;;
        }

        public ActionResult Index()
        {
           var rooms = _roomservice.Rooms();
            return View(rooms);
        }

        public ActionResult Details(string id)
        {
            var room = _roomservice.Read(id);

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
                _roomservice.Add(room);

                return RedirectToAction(nameof(Details), new { id = room.Id });
            }
            return View(room);
        }

        public ActionResult Edit(string id)
        {
            var room = _roomservice.edit(id);

            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _roomservice.edit(id,room);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        public ActionResult Delete(string id)
        {
            var room = _roomservice.edit(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            // var room = _dbContext.Room.FirstOrDefault(r => r.Id == id);
            var room = _roomservice.edit(id);
            if (room != null)
            {
                _roomservice.Delete(id);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
