using Microsoft.AspNetCore.Mvc;
using Hotel_Room_Reservation_System_Test.Databases;
using Hotel_Room_Reservation_System_Test.Models;
using System.Security.Claims;

namespace Hotel_Room_Reservation_System_Test.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HotelDbContext _dbContext;

        public ReservationController(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var reservations = _dbContext.Reservation.ToList();
            return View(reservations);
        }


        public ActionResult Details(int id)
        {
            var reservation = _dbContext.Reservation.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            UpdateReservationStatus(reservation);
            return View(reservation);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Reservation.Add(reservation);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = reservation.Id });
            }
            return View(reservation);
        }

        public ActionResult Edit(int id)
        {
            var reservation = _dbContext.Reservation.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Reservation.Update(reservation);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public ActionResult Delete(int id)
        {
            var reservation = _dbContext.Reservation.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var reservation = _dbContext.Reservation.FirstOrDefault(r => r.Id == id);
            if (reservation != null)
            {

                _dbContext.Reservation.Remove(reservation);
                _dbContext.SaveChanges();
            }          
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Reserve(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            // Retrieve user ID from claims
            var userIdString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Index", "Home"); 
            }

            var reservation = new Reservation
            {
                RoomId = roomId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                UserId = userId,
            };

            _dbContext.Reservation.Add(reservation);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        private void UpdateReservationStatus(Reservation reservation)
        {
            var currentDate = DateTime.Now;

            // Check if reservation is expired
            if (reservation.CheckOutDate < currentDate && reservation.Status == "Reserved")
            {
                // Update room availability
                var room = _dbContext.Room.FirstOrDefault(r => r.Id == reservation.RoomId);
                if (room != null)
                {
                    room.IsAvailable = true;
                }

                // Update reservation status
                reservation.Status = "Expired";

                // Save changes to the database
                _dbContext.SaveChanges();
            }
        }
    }
}
