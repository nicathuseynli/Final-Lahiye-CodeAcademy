using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Controllers;
public class AccountController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<AccountController> _logger;

    public AccountController(AppDbContext context, ILogger<AccountController> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task Index()
    {

    }

    public async Task<IActionResult> Login()
    {
        var login = await _context.LoginPages.FirstOrDefaultAsync();
       
        LoginRegisterVM loginVM = new()
        {
            LoginPage = login,
        };
        return View(loginVM);
    }

    public async Task<IActionResult> Register()
    {
        var register = await _context.RegisterPages.FirstOrDefaultAsync();

        LoginRegisterVM registerVM = new()
        {
            RegisterPage = register,
        };
        return View(registerVM);
    }
}
