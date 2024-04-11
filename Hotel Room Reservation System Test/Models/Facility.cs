namespace Sample_Hotel_Room_Reservation_System.Models
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Facility()
        {
            
        }
        public Facility(string _name, string description)
        {

            Name = _name;
            Description = description;
        }
    }
}
