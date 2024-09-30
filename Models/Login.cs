using System.ComponentModel.DataAnnotations;

namespace GASSBOOKING_WEBSITE.Models
{
    public class Login
    {
        public int Login_Id { get; set; }
        public int Reg_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string? Password { get; set; }
        public string? Login_Type { get; set; }
    }
}
