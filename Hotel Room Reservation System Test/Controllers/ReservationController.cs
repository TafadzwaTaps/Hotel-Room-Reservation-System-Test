using Microsoft.AspNetCore.Mvc;
using Hotel_Room_Reservation_System_Test.Databases;
using Hotel_Room_Reservation_System_Test.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public IActionResult CreateReservation(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            var room = _dbContext.Room.FirstOrDefault(r => r.Id == roomId);
            if (room == null)
            {
                return NotFound();
            }

            var userId = GetCurrentUserId();

            var reservationViewModel = new ReservationViewModel
            {
                RoomId = roomId,
                RoomName = room.Name,
                UserId = userId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate
            };
             
            return View(reservationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateReservation(ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {

                // Create reservation
                var reservation = new Reservation
                {
                    RoomId = model.RoomId,
                    UserId = model.UserId,
                    CheckInDate = model.CheckInDate,
                    CheckOutDate = model.CheckOutDate,
                    Status = "Reserved"
                };

                _dbContext.Reservation.Add(reservation);
                _dbContext.SaveChanges();

                // Update room availability status
                var room = _dbContext.Room.FirstOrDefault(r => r.Id == model.RoomId);
                if (room != null)
                {
                    room.IsAvailable = false;
                    _dbContext.SaveChanges();
                }

                return RedirectToAction("Details", "Reservation", new { id = reservation.Id });
            }

            return View(model);
        }

        private int GetCurrentUserId()
        {
            // Retrieve the current user's ID from the claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? int.Parse(userId) : 0;
        }

        public IActionResult Details(int id)
        {
            var reservation = _dbContext.Reservation
                .Include(r => r.Room)
                .ThenInclude(r => r.RoomImages)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            var viewModel = new ReservationDetailsViewModel
            {
                Reservation = reservation,
                Room = reservation.Room
            };

            return View(viewModel);
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
