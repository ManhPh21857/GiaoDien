using Microsoft.AspNetCore.Mvc;

namespace WebClientAdmin.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult ManageEmployee()
        {
            return View();
        }
        public IActionResult AddEmployee()
        {
            return View();
        }
    }
}
