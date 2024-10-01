using GASSBOOKING_WEBSITE.Interface;
using GASSBOOKING_WEBSITE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            ViewData["Message"] = bookings == null || !bookings.Any() ? "No bookings found." : null;

            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> BookGas(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CustomerDashboard");
            }

            booking.Customer_Reg_Id = GetCustomerRegId(User);
            bool result = await _bookingService.AddBookingAsync(booking);

            TempData["Message"] = result ? "Booking successful!" : "Booking failed. Please try again.";
            return RedirectToAction("CustomerDashboard");
        }

        private int GetCustomerRegId(ClaimsPrincipal user)
        {
            var customerRegIdClaim = user.FindFirst("RegisterId")?.Value;
            return customerRegIdClaim != null ? int.Parse(customerRegIdClaim) : 0;
        }

        public async Task<IActionResult> BookingHistory()
        {
            var bookings = await _bookingService.GetBookingHistoryAsync(User.Identity.Name);
            return View(bookings);
        }
    }
}
