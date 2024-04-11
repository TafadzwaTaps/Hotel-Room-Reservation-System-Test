namespace Sample_Hotel_Room_Reservation_System.Models
{
    public class UserRole
    {
        public int UserId { get; set; }
        public string RoleId { get; set; } 
        public UserRole() 
        { 
        
        }

        public UserRole(string _roleID)
        {
            RoleId = _roleID;
        }
    }
}
