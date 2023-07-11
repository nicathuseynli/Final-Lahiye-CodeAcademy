using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.LoginPage;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class LoginPageController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<LoginPageController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILoginPageService _loginPageService;
    public LoginPageController(AppDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<LoginPageController> logger, ILoginPageService loginPageService)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
        _loginPageService = loginPageService;
    }
    public async Task<IActionResult> Index()
    {
        var loginPage = await _context.LoginPages.ToListAsync();
        return View(loginPage);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLoginPageVM createLoginVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createLoginVM.Photo.ContentType.Contains("image/")) return View();

        if (createLoginVM.Photo.Length / 1024 > 500) return View();

        await _loginPageService.CreateAsync(createLoginVM);

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var delete = await _loginPageService.GetByIdAsync(id);
        return View(delete);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var delete = _loginPageService.DeleteAsync(id);
        if (delete == null) return View();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var loginpage = await _context.LoginPages.FirstOrDefaultAsync(x => x.Id == id);
        if (loginpage == null) return NotFound();

        var updateLoginVM = new UpdateLoginPageVM()
        {
            Id = loginpage.Id,
            Description = loginpage.Description,
            Image = loginpage.Image,
        };
        return View(updateLoginVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateLoginPageVM updateLoginVM)
    {
        var result = await _loginPageService.UpdateAsync(updateLoginVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
