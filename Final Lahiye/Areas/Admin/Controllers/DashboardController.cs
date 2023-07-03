using Microsoft.AspNetCore.Mvc;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
