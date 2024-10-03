using GASSBOOKING_WEBSITE.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GASSBOOKING_WEBSITE.Repository
{
    public class BookingRepository
    {
        private readonly string _connectionString = @"server=LAPTOP-0LIDVBVS\SQLEXPRESS;Database=GasBooking;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";

        public async Task<bool> AddBookingAsync(Booking booking)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("AddBooking", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Cylinder_Id", booking.Cylinder_Id);
                    command.Parameters.AddWithValue("@Customer_Reg_Id", booking.Customer_Reg_Id);
                    command.Parameters.AddWithValue("@Staff_Reg_Id", booking.Staff_Reg_Id as object ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Booking_Date", booking.Booking_Date);
                    command.Parameters.AddWithValue("@Booking_Status", booking.Booking_Status);
                    command.Parameters.AddWithValue("@Booking_Mode", booking.Booking_Mode);
                    var outputParam = new SqlParameter("@NewBookingId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    connection.Open();

                    await command.ExecuteNonQueryAsync();

                    int returnValue = (int)command.Parameters["@NewBookingId"].Value;
                    booking.Booking_Id = returnValue;

                    return returnValue > 0;
                }
            }
        }

        public async Task<IEnumerable<Booking>> GetBookingHistoryAsync(string customerUsername)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetBookingHistory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", customerUsername);
                    connection.Open();

                    var bookings = new List<Booking>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var booking = new Booking
                            {                                Cylinder_Type = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                                Customer_Reg_Id = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                Staff_Reg_Id = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                                Booking_Date = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                                Booking_Status = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                Booking_Mode = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                            };
                            bookings.Add(booking);
                        }
                    }
                    return bookings;
                }
            }
        }

        public List<Booking> GetAllBookings()
        {
            var bookings = new List<Booking>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetAllBookings", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bookings.Add(new Booking
                            {
                                Booking_Id = reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0),
                                Cylinder_Id = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                Customer_Reg_Id = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                Staff_Reg_Id = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                                Booking_Date = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                Booking_Status = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Booking_Mode = reader.IsDBNull(6) ? null : reader.GetString(6)
                            });
                        }
                    }
                }
            }

            return bookings;
        }

    }
}