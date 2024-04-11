using Microsoft.AspNetCore.Mvc;
using Sample_Hotel_Room_Reservation_System.Databases;
using Sample_Hotel_Room_Reservation_System.Models;
using System.Linq;

namespace Sample_Hotel_Room_Reservation_System.Controllers
{
    public class FacilityController : Controller
    {
        private readonly HotelDbContext _dbContext;

        public FacilityController(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var facilities = _dbContext.Facility.ToList();
            return View(facilities);
        }

        public ActionResult Details(int id)
        {
            var facility = _dbContext.Facility.FirstOrDefault(f => f.Id == id);
            if (facility == null)
            {
                return NotFound();
            }
            return View(facility);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Facility facility)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Facility.Add(facility);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = facility.Id });
            }
            return View(facility);
        }

        public ActionResult Edit(int id)
        {
            var facility = _dbContext.Facility.FirstOrDefault(f => f.Id == id);
            if (facility == null)
            {
                return NotFound();
            }
            return View(facility);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Facility facility)
        {
            if (id != facility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Facility.Update(facility);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(facility);
        }

        public ActionResult Delete(int id)
        {
            var facility = _dbContext.Facility.FirstOrDefault(f => f.Id == id);
            if (facility == null)
            {
                return NotFound();
            }
            return View(facility);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var facility = _dbContext.Facility.FirstOrDefault(f => f.Id == id);
            _dbContext.Facility.Remove(facility);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
