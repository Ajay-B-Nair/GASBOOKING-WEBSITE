using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GASSBOOKING_WEBSITE.Interface;
using GASSBOOKING_WEBSITE.Models;

namespace GASSBOOKING_WEBSITE.Controllers
{
    public class StaffController : Controller
    {
        private readonly IBookingService _bookingService;

        public StaffController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [Authorize(Roles = "staff")]
        public IActionResult StaffDashboard()
        {
            var bookings = _bookingService.GetAllBookings();
            return View(bookings);
        }
    }
}
