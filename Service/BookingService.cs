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
    }
}
