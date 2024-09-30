using GASSBOOKING_WEBSITE.Interface;
using GASSBOOKING_WEBSITE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace GASSBOOKING_WEBSITE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegistrationService _registrationService;

        public HomeController(ILogger<HomeController> logger, IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration(string type)
        {
            if (type == "staff")
            {
                return View("StaffRegistration");
            }
            else if (type == "customer")
            {
                return View("CustomerRegistration");
            }
            return RedirectToAction("Index");
        }

        public IActionResult StaffRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterStaff(Staff staff, string userName, string password)
        {
            if (ModelState.IsValid)
            {
                staff.Staff_Status = "Active";

                var login = new Login
                {
                    UserName = userName,
                    Password = password,
                    Login_Type = "staff"
                };

                string result = _registrationService.RegisterStaff(staff, login);
                ViewBag.Message = result;
                return RedirectToAction("Index");
            }
            return View("StaffRegistration", staff);
        }


        public IActionResult CustomerRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterCustomer(Customer customer, string userName, string password)
        {
            if (ModelState.IsValid)
            {
                customer.Customer_Status = "Active";

                var login = new Login
                {
                    UserName = userName,
                    Password = password,
                    Login_Type = "customer"
                };

                string result = _registrationService.RegisterCustomer(customer, login);
                ViewBag.Message = result;
                return RedirectToAction("Index");
            }

            return View("CustomerRegistration", customer);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
