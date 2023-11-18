using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class OnlineSalesController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        public IActionResult CartNull()
        {
            return View();
        }
    }
}
