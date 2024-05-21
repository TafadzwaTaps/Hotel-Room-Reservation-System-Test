using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Hotel_Room_Reservation_System_Test.Models;

public class RoomImages
{
    public int Id { get; set; }
    public int RoomId { get; set; } // Add this line
    public string? ImageUrl { get; set; }

    [NotMapped] // This tells EF Core not to map this property to the database
    [Required(ErrorMessage = "Please select a file.")]
    [Display(Name = "Image File")]
    public IFormFile? ImageFile { get; set; }

    public RoomImages() 
    {
    
    }
}

