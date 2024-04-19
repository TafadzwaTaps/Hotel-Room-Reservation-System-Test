namespace Hotel_Room_Reservation_System_Test.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Role()
        {
            
        }

        public Role(string _name)
        {
            Name = _name;
        }
    }
}
