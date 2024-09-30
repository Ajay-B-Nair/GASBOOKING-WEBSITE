using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GASSBOOKING_WEBSITE.Controllers
{
    public class StaffController : Controller
    {
        [Authorize(Roles = "staff")]
        public IActionResult StaffDashboard()
        {
            return View();
        }
    }
}
