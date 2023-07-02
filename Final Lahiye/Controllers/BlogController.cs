using Microsoft.AspNetCore.Mvc;

namespace Final_Lahiye.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
