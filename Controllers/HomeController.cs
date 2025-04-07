using Microsoft.AspNetCore.Mvc;

namespace CLDV6211_Part1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
