using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Controllers;
public class LoginRegisterController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<LoginRegisterController> _logger;

    public LoginRegisterController(AppDbContext context, ILogger<LoginRegisterController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var login = await _context.LoginPages.FirstOrDefaultAsync();
        var register = await _context.RegisterPages.FirstOrDefaultAsync();

        LoginRegisterVM loginRegisterVM = new()
        {
            LoginPage = login,
            RegisterPage = register,
        };
        return View(loginRegisterVM);
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
