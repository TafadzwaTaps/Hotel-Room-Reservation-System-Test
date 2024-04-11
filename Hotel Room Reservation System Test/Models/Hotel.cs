namespace Sample_Hotel_Room_Reservation_System.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }

        public Hotel()
        {
            
        }

        public Hotel(string _name, string _address, int _phoneNumber)
        {
            Name = _name;
            Address = _address;
            PhoneNumber = _phoneNumber;
        }
    }
}
