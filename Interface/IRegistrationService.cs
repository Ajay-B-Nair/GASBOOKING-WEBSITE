using GASSBOOKING_WEBSITE.Models;

namespace GASSBOOKING_WEBSITE.Interface
{
    public interface IRegistrationService
    {
        string RegisterStaff(Staff staff, Login login);
        string RegisterCustomer(Customer customer, Login login);
    }
}
