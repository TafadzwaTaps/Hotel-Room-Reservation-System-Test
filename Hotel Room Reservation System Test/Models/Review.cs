using System.ComponentModel.DataAnnotations;

namespace Hotel_Room_Reservation_System_Test.Models
{
    public class Review
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int RoomID { get; set; }
        [Required]
        public string? UserID { get; set; }
        [Required]
        public int Rating { get; set; } 
        [Required]
        public string? Comment { get; set; }
        public DateTime ReviewDate{ get; set; }

        public Review()
        {
            Rating = 0;
        }
        public Review(int _roomID, string _userID, int _rating, string _comment, DateTime _reviewDate)
        {
            RoomID = _roomID;
            UserID = _userID;
            Rating = _rating;
            Comment = _comment;
            ReviewDate = _reviewDate;
        }

    }
}
