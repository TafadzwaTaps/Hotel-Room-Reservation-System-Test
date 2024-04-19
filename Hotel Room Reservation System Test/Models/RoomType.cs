namespace Hotel_Room_Reservation_System_Test.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public RoomType()
        {
            
        }

        public RoomType(string _name, string _description)
        {
            Name = _name;
            Description = _description;
        }
    }
}
