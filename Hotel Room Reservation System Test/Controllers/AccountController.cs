using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Hotel_Room_Reservation_System_Test.Databases;
using Hotel_Room_Reservation_System_Test.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AccountController : Controller
{
    private readonly HotelDbContext _dbContext;
    public AccountController(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly List<User> users = new List<User>();

    // Other methods...

    private string HashPassword(string password)
    {

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    private bool IsUsernameTaken(string username)
    {
        return _dbContext.User.Any(user => user.UserName == username);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterView user)
    {
        if (ModelState.IsValid)
        {
            // Check if the provided username is not null or empty
            if (!string.IsNullOrEmpty(user.Username))
            {
                bool isUsernameTaken = IsUsernameTaken(user.Username);

                if (isUsernameTaken)
                {
                    ModelState.AddModelError("Username", "The username is already taken.");
                    return View(user);
                }

                var newUser = new User
                {
                    UserName = user.Username,
                    Email = user.Email,
                    PasswordHash = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    PhysicalAddress = user.PhysicalAddress,
                    Role = user.Role,
                };

                _dbContext.User.Add(newUser); // Add the newUser object
                _dbContext.SaveChanges();

                // Redirect or perform other actions after successful registration.
                return RedirectToAction("Login"); // Customize this as needed.
            }
            else
            {
                // Handle the case where the username is null or empty
                ModelState.AddModelError("Username", "Username cannot be null or empty.");
                return View(user);
            }
        }

        return View(user);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginView model)
    {
        if (ModelState.IsValid)
        {
            var hashedPassword = (model.Password); // Hash the provided password
            var user = _dbContext.User.FirstOrDefault(u => u.UserName == model.Username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, model.Username ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role ?? string.Empty)
                };

                var identity = new ClaimsIdentity(claims, "login");
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(principal, props);

                if (!string.IsNullOrEmpty(user.UserName))
                {
                    HttpContext.Session.SetString("Username", user.UserName);
                }
                if (!string.IsNullOrEmpty(user.Role))
                {
                    HttpContext.Session.SetString("Role", user.Role);
                }
                // Redirect to a success page after setting cookies or session variables
                return RedirectToAction("HomePage", "home");
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        return View(model);
    }


}