using Microsoft.AspNetCore.Mvc;

namespace Final_Lahiye.Controllers
{
    public class SingleBlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
