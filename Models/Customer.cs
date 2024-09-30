namespace GASSBOOKING_WEBSITE.Models
{
    public class Customer
    {
        public int Customer_Reg_Id { get; set; }
        public string? Customer_Name { get; set; }
        public long Consumer_Number { get; set; }
        public string? Customer_Address { get; set; }
        public long Customer_Phone { get; set; }
        public string? Customer_Email { get; set; }
        public string Customer_Status { get; set; } = "Active";
    }
}
