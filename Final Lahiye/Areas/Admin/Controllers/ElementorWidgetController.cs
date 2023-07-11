using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class ElementorWidgetController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ElementorWidgetController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IElementorService _elementorService;

    public ElementorWidgetController(AppDbContext context, ILogger<ElementorWidgetController> logger, IWebHostEnvironment webHostEnvironment, IElementorService elementorService)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        _elementorService = elementorService;
    }

    public async Task<IActionResult> Index()
    {
        var elementorWidget = await _context.Elementors.ToListAsync();
        return View(elementorWidget);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateElementorWidgetVM createElementorWidgetVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createElementorWidgetVM.ElementorUpPhoto.ContentType.Contains("image/")) return View();

        if (!createElementorWidgetVM.ElementorDownPhoto.ContentType.Contains("image/")) return View();

        if (createElementorWidgetVM.ElementorUpPhoto.Length / 1024 > 500) return View();

        if (createElementorWidgetVM.ElementorDownPhoto.Length / 1024 > 500) return View();

        await _elementorService.CreateAsync(createElementorWidgetVM);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var details = await _elementorService.GetByIdAsync(id);

        return View(details);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var delete = _elementorService.DeleteAsync(id);
        if (delete == null) return View();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var elementor = await _context.Elementors.FirstOrDefaultAsync(x => x.Id == id);
        if (elementor == null) return NotFound();

        var updateElementorVM = new UpdateElementorWidgetVM()
        {
            Id = elementor.Id,
            ElementorUpImage = elementor.ElementorUpImage,
            ElementorDownImage = elementor.ElementorDownImage,
        };
        return View(updateElementorVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateElementorWidgetVM updateElementorWidgetVM)
    {
        var result = await _elementorService.UpdateAsync(updateElementorWidgetVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
