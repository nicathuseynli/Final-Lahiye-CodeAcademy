using Microsoft.AspNetCore.Mvc;

namespace Final_Lahiye.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
