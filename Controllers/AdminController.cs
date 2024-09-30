using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GASSBOOKING_WEBSITE.Models;
using GASSBOOKING_WEBSITE.Interface;

namespace GASSBOOKING_WEBSITE.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ICylinderService _cylinderService;

        public AdminController(ICylinderService cylinderService)
        {
            _cylinderService = cylinderService;
        }

        [HttpGet]
        public IActionResult AdminDashboard()
        {
            var cylinders = _cylinderService.GetAllCylinders();
            return View(cylinders);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Cylinder cylinder)
        {
            if (ModelState.IsValid)
            {
                _cylinderService.UpdateCylinder(cylinder);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _cylinderService.DeleteCylinder(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Add(Cylinder cylinder)
        {
            if (ModelState.IsValid)
            {
                _cylinderService.AddCylinder(cylinder);
                return Ok();
            }
            return BadRequest();
        }
    }
}
