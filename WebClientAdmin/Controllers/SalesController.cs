using Microsoft.AspNetCore.Mvc;

namespace WebClientAdmin.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SalesReport()
        {
            return View();
        }
        public IActionResult OrderManage()
        {
            return View();
        }
    }
}
