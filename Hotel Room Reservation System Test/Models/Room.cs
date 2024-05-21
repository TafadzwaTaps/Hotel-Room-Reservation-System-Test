using System.ComponentModel.DataAnnotations;

namespace Hotel_Room_Reservation_System_Test.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int RoomCapacity { get; set; } = 0;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsAvailable { get; set; }

        public ICollection<RoomImages> RoomImages { get; set; }

        public Room()
        {
            RoomCapacity = 0;
            Price = 0;
        }

        public Room(string _name, string _description, int _roomCapacity, decimal _price, bool _isAvailable)
        {
            Name = _name;
            Description = _description;
            RoomCapacity = _roomCapacity;
            Price = _price;
            IsAvailable = _isAvailable;
        }
    }
}
