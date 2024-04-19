using Microsoft.AspNetCore.Mvc;
using Hotel_Room_Reservation_System_Test.Databases;
using Hotel_Room_Reservation_System_Test.Models;
using System.Linq;

namespace Hotel_Room_Reservation_System_Test.Controllers
{
    public class InvoiceGenerationController : Controller
    {
        private readonly HotelDbContext _dbContext;

        public InvoiceGenerationController(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var invoices = _dbContext.InvoiceGeneration.ToList();
            return View(invoices);
        }

        public ActionResult Details(int id)
        {
            var invoice = _dbContext.InvoiceGeneration.FirstOrDefault(i => i.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceGeneration invoice)
        {
            if (ModelState.IsValid)
            {
                _dbContext.InvoiceGeneration.Add(invoice);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = invoice.Id });
            }
            return View(invoice);
        }

        public ActionResult Edit(int id)
        {
            var invoice = _dbContext.InvoiceGeneration.FirstOrDefault(i => i.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InvoiceGeneration invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.InvoiceGeneration.Update(invoice);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        public ActionResult Delete(int id)
        {
            var invoice = _dbContext.InvoiceGeneration.FirstOrDefault(i => i.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var invoice = _dbContext.InvoiceGeneration.FirstOrDefault(i => i.Id == id);
            if (invoice != null)
            {
                _dbContext.InvoiceGeneration.Remove(invoice);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
