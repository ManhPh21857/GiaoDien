using Microsoft.AspNetCore.Mvc;

namespace WebClientAdmin.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
