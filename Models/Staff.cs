namespace GASSBOOKING_WEBSITE.Models
{
    public class Staff
    {
        public int Staff_Reg_Id { get; set; }
        public string? Staff_Name { get; set; }
        public long Staff_Phone { get; set; }
        public string? Staff_Email { get; set; }
        public string Staff_Status { get; set; } = "Active";
    }
}
