using GASSBOOKING_WEBSITE.Interface;
using GASSBOOKING_WEBSITE.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GASSBOOKING_WEBSITE.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public LoginController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public IActionResult AuthLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AuthLogin(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            if (_authorizationService.ValidateUserService(login.UserName, login.Password, out string userType, out int registerId))
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, login.UserName),
            new Claim(ClaimTypes.Role, userType),
            new Claim("RegisterId", registerId.ToString()) // Store register ID in claims
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                switch (userType.ToLower())
                {
                    case "admin":
                        return RedirectToAction("AdminDashboard", "Admin");
                    case "staff":
                        return RedirectToAction("StaffDashboard", "Staff");
                    case "customer":
                        return RedirectToAction("CustomerDashboard", "Customer");
                    default:
                        ModelState.AddModelError("", "Invalid user type.");
                        return View(login);
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("AuthLogin", "Login");
        }
    }
}
