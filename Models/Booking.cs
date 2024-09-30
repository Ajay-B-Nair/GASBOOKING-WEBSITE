namespace GASSBOOKING_WEBSITE.Models
{
    public class Booking
    {
        public int? Booking_Id { get; set; }
        public int? Cylinder_Id { get; set; }
        public string? Cylinder_Type { get; set; }
        public int? Customer_Reg_Id { get; set; }
        public int? Staff_Reg_Id { get; set; }
        public DateTime? Booking_Date { get; set; }
        public string? Booking_Status { get; set; }
        public string? Booking_Mode { get; set; }
    }
}

