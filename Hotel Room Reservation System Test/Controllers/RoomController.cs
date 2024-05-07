using Microsoft.AspNetCore.Mvc;
using Hotel_Room_Reservation_System_Test.Databases;
using Hotel_Room_Reservation_System_Test.Models;
using System.Linq;
using System.Security.Claims;

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

        public IActionResult Reserve(int roomId, DateTime checkInDate, DateTime checkOutDate, string status)
        {
            var room = _dbContext.Room.FirstOrDefault(r => r.Id == roomId);
            if (room == null)
            {
                return NotFound(); // Room not found
            }

            // Check room availability for the specified dates
            var isAvailable = IsRoomAvailable(roomId, checkInDate, checkOutDate);
            if (!isAvailable)
            {
                // Handle room not available scenario (e.g., display error message)
                return View("RoomNotAvailable", roomId);
            }

            // Get the current user's ID
            var userId = GetCurrentUserId();

            // Create reservation with default status if status is not provided
            if (string.IsNullOrEmpty(status))
            {
                status = "Reserved"; // You can set a default status here
            }

            // Create reservation
            var reservation = new Reservation
            {
                RoomId = roomId,
                UserId = userId, // Assign the user's ID to the reservation
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                Status = status
                // You may need to include more details like user ID, etc.
            };

            _dbContext.Reservation.Add(reservation);
            _dbContext.SaveChanges();

            // Update room availability status
            room.IsAvailable = false;
            _dbContext.SaveChanges();

            // Redirect to a confirmation page or display a success message
            return RedirectToAction("ReservationConfirmation", new { reservationId = reservation.Id });
        }

        private int GetCurrentUserId()
        {
            // Retrieve the current user's ID from the claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? int.Parse(userId) : 0;
        }


        private bool IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            // Check if there are any existing reservations that overlap with the specified dates
            var overlappingReservations = _dbContext.Reservation
                .Any(r => r.RoomId == roomId &&
                          ((r.CheckInDate <= checkInDate && r.CheckOutDate >= checkInDate) ||
                           (r.CheckInDate <= checkOutDate && r.CheckOutDate >= checkOutDate)));

            // Room is available if there are no overlapping reservations
            return !overlappingReservations;
        }
    }
}
