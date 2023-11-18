using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Information()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
    }
}
