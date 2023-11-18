using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult ListProduct()
        {
            return View();
        }
        public IActionResult ProductDetail()
        {
            return View();
        }
        public IActionResult SearchProduct()
        {
            return View();
        }
    }
}
