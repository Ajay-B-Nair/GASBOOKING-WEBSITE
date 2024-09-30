using GASSBOOKING_WEBSITE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GASSBOOKING_WEBSITE.Interface
{
    public interface IBookingService
    {
        Task<bool> AddBookingAsync(Booking booking);  // Ensure this method is defined
        Task<IEnumerable<Booking>> GetBookingHistoryAsync(string username);
    }
}