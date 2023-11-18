using Microsoft.AspNetCore.Mvc;

namespace WebClientAdmin.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
        }
    }
}
