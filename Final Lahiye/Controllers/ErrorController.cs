using Microsoft.AspNetCore.Mvc;

namespace Final_Lahiye.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
