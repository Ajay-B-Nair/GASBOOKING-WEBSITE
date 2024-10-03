using GASSBOOKING_WEBSITE.Interface;
using GASSBOOKING_WEBSITE.Models;
using GASSBOOKING_WEBSITE.Repository;

namespace GASSBOOKING_WEBSITE.Service
{
    public class RegistrationService : IRegistrationService
    {
        private readonly RegistrationRepository _repository;

        public RegistrationService()
        {
            _repository = new RegistrationRepository();
        }

        public string RegisterStaff(Staff staff, Login login)
        {
            return _repository.InsertStaff(staff, login);
        }

        public string RegisterCustomer(Customer customer, Login login)
        {
            return _repository.InsertCustomer(customer, login);
        }
    }
}