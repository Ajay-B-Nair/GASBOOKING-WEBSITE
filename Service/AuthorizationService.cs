using GASSBOOKING_WEBSITE.Interface;
using GASSBOOKING_WEBSITE.Repositories;

namespace GASSBOOKING_WEBSITE.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly AuthorizationRepository _authorizationRepository;

        public AuthorizationService(AuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public bool ValidateUserService(string userName, string password, out string userType, out int registerId)
        {
            return _authorizationRepository.ValidateUser(userName, password, out userType, out registerId);
        }
    }
}
