using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class BannerController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<BannerController> _logger;
    private readonly IBannerService _bannerService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public BannerController(AppDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<BannerController> logger, IBannerService bannerService)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
        _bannerService = bannerService;
    }
    public async Task<IActionResult> Index()
    {
        var banner = await _context.Banners.ToListAsync();
        return View(banner);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBannerVM createbannerVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createbannerVM.BannerPhoto.ContentType.Contains("image/")) return View();

        if (createbannerVM.BannerPhoto.Length / 1024 > 500) return View();

        await _bannerService.CreateAsync(createbannerVM);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var details = await _bannerService.GetByIdAsync(id);
        return View(details);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == id);
        if (banner == null) return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", banner.BannerImage);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);
        _context.Banners.Remove(banner);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == id);
        if (banner == null) return NotFound();

        var updateBanner = new UpdateBannerVM()
        {
            Id = banner.Id,
            Title = banner.Title,
            Image = banner.BannerImage,
        };
        return View(updateBanner);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateBannerVM updateBannerVM)
    {
        var result = await _bannerService.UpdateAsync(updateBannerVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
