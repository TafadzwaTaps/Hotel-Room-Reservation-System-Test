using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hotel_Room_Reservation_System_Test.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role))
            {
                // Redirect to login page if session is invalid
                context.Result = RedirectToAction("Login", "Account");
            }

            base.OnActionExecuting(context);
        }
    }
}
