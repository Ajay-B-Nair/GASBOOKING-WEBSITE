namespace GASSBOOKING_WEBSITE.Interface
{
    public interface IAuthorizationService
    {
        bool ValidateUserService(string userName, string password, out string userType, out int registerId);
    }
}
