using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Error;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class ErrorPageController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ErrorPageController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IErrorService _errorPageService;

    public ErrorPageController(AppDbContext context, ILogger<ErrorPageController> logger, IWebHostEnvironment webHostEnvironment, IErrorService errorPageService)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        _errorPageService = errorPageService;
    }

    public async Task<IActionResult> Index()
    {
        var errorPage = await _context.ErrorPages.ToListAsync();
        return View(errorPage);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateErrorPageVM createErrorPageVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createErrorPageVM.Photo.ContentType.Contains("image/")) return View();

        if (createErrorPageVM.Photo.Length / 1024 > 500) return View();

        await _errorPageService.CreateAsync(createErrorPageVM);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var errorpage = await _errorPageService.GetByIdAsync(id);

        return View(errorpage);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync(x => x.Id == id);
        if (errorPage == null) return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", errorPage.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.ErrorPages.Remove(errorPage);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync(x => x.Id == id);
        if (errorPage == null) return NotFound();

        var updateErrorPageVM = new UpdateErrorPageVM()
        {
            Id = errorPage.Id,
            Message = errorPage.Message,
            Image = errorPage.Image,
        };
        return View(updateErrorPageVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateErrorPageVM updateErrorPageVM)
    {
        var result = await _errorPageService.UpdateAsync(updateErrorPageVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
