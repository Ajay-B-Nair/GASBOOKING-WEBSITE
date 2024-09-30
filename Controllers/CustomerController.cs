using GASSBOOKING_WEBSITE.Interface;
using GASSBOOKING_WEBSITE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GASSBOOKING_WEBSITE.Controllers
{
    [Authorize(Roles = "customer")]
    public class CustomerController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ICylinderService _cylinderService;

        public CustomerController(IBookingService bookingService, ICylinderService cylinderService)
        {
            _bookingService = bookingService;
            _cylinderService = cylinderService;
        }

        public async Task<IActionResult> CustomerDashboard()
        {
            var bookings = await _bookingService.GetBookingHistoryAsync(User.Identity.Name);

            var cylinderTypes = await _cylinderService.GetCylinderTypesAsync();
            ViewBag.CylinderTypes = cylinderTypes;

            if (bookings == null || !bookings.Any())
            {
                ViewData["Message"] = "No bookings found.";
            }

            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> BookGas(Booking booking)
        {
            ViewBag.CylinderTypes = await _cylinderService.GetCylinderTypesAsync();

            if (ModelState.IsValid)
            {
                booking.Customer_Reg_Id = GetCustomerRegId(User.Identity.Name);

                bool result = await _bookingService.AddBookingAsync(booking);

                TempData["Message"] = result ? "Booking successful!" : "Booking failed. Please try again.";
                return RedirectToAction("CustomerDashboard");
            }

            var bookings = await _bookingService.GetBookingHistoryAsync(User.Identity.Name);
            return View("CustomerDashboard", bookings);
        }



        private int GetCustomerRegId(string username)
        {
            return 1;
        }

        public async Task<IActionResult> BookingHistory()
        {
            var bookings = await _bookingService.GetBookingHistoryAsync(User.Identity.Name);
            return View(bookings);
        }
    }
}
