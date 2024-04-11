using System.ComponentModel.DataAnnotations;

namespace Sample_Hotel_Room_Reservation_System.Models
{
    public class RegisterView
    {
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [Display(Name = "FirstName")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [Display(Name = "LastName")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }

        public string? PhysicalAddress { get; set; }

        public string? Role { get; set; }
    }
}
