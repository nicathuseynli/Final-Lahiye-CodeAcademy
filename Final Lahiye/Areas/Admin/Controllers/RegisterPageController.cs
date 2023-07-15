using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.RegisterPage;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class RegisterPageController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<RegisterPageController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IRegisterPageService _registerService;

    public RegisterPageController(AppDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<RegisterPageController> logger, IRegisterPageService registerService)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
        _registerService = registerService;
    }
    public async Task<IActionResult> Index()
    {
        var registerPage = await _context.RegisterPages.ToListAsync();
        return View(registerPage);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateRegisterPageVM createRegisterPageVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createRegisterPageVM.Photo.ContentType.Contains("image/")) return View();

        if (createRegisterPageVM.Photo.Length / 1024 > 500) return View();

        await _registerService.CreateAsync(createRegisterPageVM);

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var delete = await _registerService.GetByIdAsync(id);

        return View(delete);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var registerPage = await _context.RegisterPages.FirstOrDefaultAsync(x => x.Id == id);
        if (registerPage == null) return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", registerPage.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.RegisterPages.Remove(registerPage);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var registerPage = await _context.RegisterPages.FirstOrDefaultAsync(x => x.Id == id);
        if (registerPage == null) return NotFound();

        var updateRegisterVM = new UpdateRegisterPageVM()
        {
            Id = registerPage.Id,
            Description = registerPage.Description,
            Image = registerPage.Image,
        };
        return View(updateRegisterVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRegisterPageVM updateRegisterVM)
    {
        var result = await _registerService.UpdateAsync(updateRegisterVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
