using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotel_Room_Reservation_System_Test.Models
{
    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string  Id { get; set; }
        
        public int userId { get; set; }
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
