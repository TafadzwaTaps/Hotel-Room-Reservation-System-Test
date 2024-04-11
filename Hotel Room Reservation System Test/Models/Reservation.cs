using System.ComponentModel.DataAnnotations;

namespace Sample_Hotel_Room_Reservation_System.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        public string UserId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public string Status { get; set; }
        // Navigation property
        public virtual Room Room { get; set; }

        public Reservation()
        {
   
        }

        public Reservation(int _roomID, string _userID, DateTime _checkInDate, DateTime _checkoutDate, string _status)
        {
            RoomId = _roomID;
            UserId = _userID;
            CheckInDate = _checkInDate;
            CheckOutDate = _checkoutDate;
            Status = _status;
        }
    }
}
