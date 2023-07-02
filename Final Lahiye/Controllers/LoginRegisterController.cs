using Microsoft.AspNetCore.Mvc;

namespace Final_Lahiye.Controllers
{
    public class LoginRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        } 
        public IActionResult Register()
        {
            return View();
        }
    }
}
