using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GASSBOOKING_WEBSITE.Interface;

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

        [HttpPost]
        [Authorize(Roles = "staff")]
        public async Task<IActionResult> AcceptBooking(int bookingId, int staffRegId)
        {
            var result = await _bookingService.AcceptBookingAsync(bookingId, staffRegId);
            if (result)
            {
                TempData["SuccessMessage"] = "Booking accepted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to accept booking.";
            }

            return RedirectToAction("StaffDashboard");
        }

        [Authorize(Roles = "staff")]
        public IActionResult AcceptedBookings()
        {
            var staffRegId = int.Parse(User.FindFirst("RegisterId")?.Value);
            var acceptedBookings = _bookingService.GetAcceptedBookingsByStaffAsync(staffRegId).Result;

            return View(acceptedBookings);
        }

        [HttpPost]
        [Authorize(Roles = "staff")]
        public async Task<IActionResult> MarkAsDelivered(int bookingId, int staffRegId)
        {
            var result = await _bookingService.MarkBookingAsDeliveredAsync(bookingId, staffRegId);
            if (result)
            {
                TempData["SuccessMessage"] = "Booking marked as delivered successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to mark booking as delivered.";
            }

            return RedirectToAction("AcceptedBookings");
        }

    }
}
