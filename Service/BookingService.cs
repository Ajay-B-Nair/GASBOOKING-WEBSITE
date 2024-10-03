using GASSBOOKING_WEBSITE.Interface;
using GASSBOOKING_WEBSITE.Models;
using GASSBOOKING_WEBSITE.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GASSBOOKING_WEBSITE.Service
{
    public class BookingService : IBookingService
    {
        private readonly BookingRepository _bookingRepository;

        public BookingService(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> AddBookingAsync(Booking booking)
        {
            return await _bookingRepository.AddBookingAsync(booking);
        }

        public async Task<IEnumerable<Booking>> GetBookingHistoryAsync(string username)
        {
            return await _bookingRepository.GetBookingHistoryAsync(username);
        }

        public List<Booking> GetAllBookings()
        {
            var allBookings = _bookingRepository.GetAllBookings();

            return allBookings.Where(b => b.Booking_Status == "Booked").ToList();
        }

        public async Task<bool> AcceptBookingAsync(int bookingId, int staffRegId)
        {
            return await _bookingRepository.UpdateBookingStatusAsync(bookingId, staffRegId, "Booking Accepted");
        }

        public async Task<IEnumerable<Booking>> GetAcceptedBookingsByStaffAsync(int staffRegId)
        {
            return await _bookingRepository.GetAcceptedBookingsByStaffAsync(staffRegId);
        }

        public async Task<bool> MarkBookingAsDeliveredAsync(int bookingId, int staffRegId)
        {
            // Update the booking status
            var updateResult = await _bookingRepository.UpdateBookingStatusAsync(bookingId, staffRegId, "Delivered");

            if (!updateResult) return false;

            // Save the delivery record
            return await _bookingRepository.SaveDeliveryRecordAsync(bookingId, staffRegId);
        }


    }
}
