using Microsoft.AspNetCore.Mvc;
using Hotel_Room_Reservation_System_Test.Databases;
using Hotel_Room_Reservation_System_Test.Models;
using System.Security.Claims;

namespace Hotel_Room_Reservation_System_Test.Controllers
{
    public class ReservationController : Controller
    {
        private readonly Reservationservice reservationservice;

        public ReservationController(Reservationservice reservationservice)
        {
            this.reservationservice = reservationservice;
        }

        public ActionResult Index()
        {
            var reservations = reservationservice.Reservations();
            return View(reservations);
        }

        public ActionResult Details(int id)
        {
            var i = id.ToString();
            var reservation = reservationservice.Read(i);
            if (reservation == null)
            {
                return NotFound();
            }
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
                reservationservice.Add(reservation);
                return RedirectToAction(nameof(Details), new { id = reservation.Id });
            }
            return View(reservation);
        }

        public ActionResult Edit(int id)
        {
            var i = id.ToString();
            var reservation = reservationservice.edit(i);
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
            var i = id.ToString();
            if (i != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                reservationservice.edit(i,reservation);
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public ActionResult Delete(int id)
        {
            var i = id.ToString();
            var reservation = reservationservice.Delete(i);
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
            var i = id.ToString();
            var reservation = reservationservice.ConfirmDelete(i);
            if (reservation != null)
            {
                reservationservice.Delete(i);
            }          
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Reserve(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            // Retrieve user ID from claims
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var reservation = new Reservation
            {
                RoomId = roomId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                UserId = userId
            };

            reservationservice.Add(reservation);

            return RedirectToAction("Index");
        }
    }
}
