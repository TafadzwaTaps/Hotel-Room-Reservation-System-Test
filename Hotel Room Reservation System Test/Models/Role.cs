namespace Sample_Hotel_Room_Reservation_System.Models
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
