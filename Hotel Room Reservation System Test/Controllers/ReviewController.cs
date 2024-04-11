using Microsoft.AspNetCore.Mvc;
using Sample_Hotel_Room_Reservation_System.Databases;
using Sample_Hotel_Room_Reservation_System.Models;
using System;
using System.Linq;

namespace Sample_Hotel_Room_Reservation_System.Controllers
{
    public class ReviewController : Controller
    {
        private readonly HotelDbContext _dbContext;

        public ReviewController(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var reviews = _dbContext.Review.ToList();
            return View(reviews);
        }

        public ActionResult Details(int id)
        {
            var review = _dbContext.Review.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                review.ReviewDate = DateTime.Now;
                _dbContext.Review.Add(review);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = review.Id });
            }
            return View(review);
        }

        public ActionResult Edit(int id)
        {
            var review = _dbContext.Review.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Review.Update(review);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        public ActionResult Delete(int id)
        {
            var review = _dbContext.Review.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var review = _dbContext.Review.FirstOrDefault(r => r.Id == id);
            _dbContext.Review.Remove(review);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
