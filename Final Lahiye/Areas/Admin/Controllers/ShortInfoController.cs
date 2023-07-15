using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class ShortInfoController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ShortInfoController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IShortInfoService _shortInfoService;

    public ShortInfoController(AppDbContext context, ILogger<ShortInfoController> logger, IWebHostEnvironment webHostEnvironment, IShortInfoService shortInfoService)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        _shortInfoService = shortInfoService;
    }

    public async Task<IActionResult> Index()
    {
        var shortInfo = await _context.ShortInformations.ToListAsync();
        return View(shortInfo);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateShortInfoVM createshortInfoVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createshortInfoVM.IconPhoto.ContentType.Contains("image/")) return View();

        if (createshortInfoVM.IconPhoto.Length / 1024 > 500) return View();

        await _shortInfoService.CreateAsync(createshortInfoVM);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var delete = await _shortInfoService.GetByIdAsync(id);

        return View(delete);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var shortInfo = await _context.ShortInformations.FirstOrDefaultAsync(x => x.Id == id);
        if (shortInfo == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", shortInfo.Icon);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.ShortInformations.Remove(shortInfo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var shortInfo = await _context.ShortInformations.FirstOrDefaultAsync(x => x.Id == id);
        if (shortInfo == null) return NotFound();

        var updateshortInfoVM = new UpdateShortInfoVM()
        {
            Id = shortInfo.Id,
            Title = shortInfo.Title,
            Information = shortInfo.Information,
            Icon = shortInfo.Icon,
        };
        return View(updateshortInfoVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateShortInfoVM updateshortInfoVM)
    {
        var result = await _shortInfoService.UpdateAsync(updateshortInfoVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
