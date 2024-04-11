using Microsoft.AspNetCore.Mvc;
using Sample_Hotel_Room_Reservation_System.Databases;
using Sample_Hotel_Room_Reservation_System.Models;
using System.Linq;

namespace Sample_Hotel_Room_Reservation_System.Controllers
{
    public class PaymentController : Controller
    {
        private readonly HotelDbContext _dbContext;

        public PaymentController(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var payments = _dbContext.Payment.ToList();
            return View(payments);
        }

        public ActionResult Details(int id)
        {
            var payment = _dbContext.Payment.FirstOrDefault(p => p.Id == id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Payment.Add(payment);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = payment.Id });
            }
            return View(payment);
        }

        public ActionResult Edit(int id)
        {
            var payment = _dbContext.Payment.FirstOrDefault(p => p.Id == id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Payment.Update(payment);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        public ActionResult Delete(int id)
        {
            var payment = _dbContext.Payment.FirstOrDefault(p => p.Id == id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var payment = _dbContext.Payment.FirstOrDefault(p => p.Id == id);
            _dbContext.Payment.Remove(payment);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
