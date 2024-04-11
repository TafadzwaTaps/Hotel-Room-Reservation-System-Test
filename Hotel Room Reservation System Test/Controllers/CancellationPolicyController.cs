using Microsoft.AspNetCore.Mvc;
using Sample_Hotel_Room_Reservation_System.Databases;
using Sample_Hotel_Room_Reservation_System.Models;
using System.Linq;

namespace Sample_Hotel_Room_Reservation_System.Controllers
{
    public class CancellationPolicyController : Controller
    {
        private readonly HotelDbContext _dbContext;

        public CancellationPolicyController(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var policies = _dbContext.CancellationPolicy.ToList();
            return View(policies);
        }

        public ActionResult Details(int id)
        {
            var policy = _dbContext.CancellationPolicy.FirstOrDefault(p => p.id == id);
            if (policy == null)
            {
                return NotFound();
            }
            return View(policy);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CancellationPolicy policy)
        {
            if (ModelState.IsValid)
            {
                _dbContext.CancellationPolicy.Add(policy);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = policy.id });
            }
            return View(policy);
        }

        public ActionResult Edit(int id)
        {
            var policy = _dbContext.CancellationPolicy.FirstOrDefault(p => p.id == id);
            if (policy == null)
            {
                return NotFound();
            }
            return View(policy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CancellationPolicy policy)
        {
            if (id != policy.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.CancellationPolicy.Update(policy);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(policy);
        }

        public ActionResult Delete(int id)
        {
            var policy = _dbContext.CancellationPolicy.FirstOrDefault(p => p.id == id);
            if (policy == null)
            {
                return NotFound();
            }
            return View(policy);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var policy = _dbContext.CancellationPolicy.FirstOrDefault(p => p.id == id);
            _dbContext.CancellationPolicy.Remove(policy);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
