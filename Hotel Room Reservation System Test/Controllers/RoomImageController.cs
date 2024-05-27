using Hotel_Room_Reservation_System_Test.Databases;
using Hotel_Room_Reservation_System_Test.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Room_Reservation_System_Test.Controllers
{
    public class RoomImageController : Controller
    {
        private readonly HotelDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoomImageController(HotelDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: RoomImageController
        public ActionResult Index()
        {
            return View();
        }

        // Action to display form for uploading images
        public ActionResult UploadImage()
        {
            return View();
        }

        // POST action to handle image upload
        [HttpPost]
        public ActionResult UploadImage(int roomId, RoomImages roomImage)
        {
            if (ModelState.IsValid && roomImage.ImageFile != null)
            {
                // Save the image to wwwroot/images folder
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imagesPath = Path.Combine(wwwRootPath, "images");

                // Ensure the images directory exists
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                string fileName = Path.GetFileNameWithoutExtension(roomImage.ImageFile.FileName);
                string extension = Path.GetExtension(roomImage.ImageFile.FileName);
                roomImage.ImageUrl = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(imagesPath, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    roomImage.ImageFile.CopyTo(fileStream);
                }

                // Set the RoomId
                roomImage.RoomId = roomId;

                // Save image URL to database
                _dbContext.RoomImages.Add(roomImage);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(roomImage);
        }

        public ActionResult List()
        {
            var roomImages = _dbContext.RoomImages.ToList();
            return View(roomImages);
        }
    }
}
