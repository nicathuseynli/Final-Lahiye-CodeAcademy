using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.ShopPage;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class ColourController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ColourController> _logger;
    private readonly IColourService _colourService;

    public ColourController(AppDbContext context, ILogger<ColourController> logger, IColourService colourService)
    {
        _context = context;
        _logger = logger;
        _colourService = colourService;
    }

    public async Task<IActionResult> Index()
    {
        var colour = await _context.Colours.ToListAsync();
        return View(colour);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateColourVM createColourVM)
    {
        await _colourService.CreateAsync(createColourVM);
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var colour = await _colourService.GetByIdAsync(id);
        return View(colour);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var delete = _colourService.DeleteAsync(id);
        if (delete == null) return View();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var colour = await _context.Colours.FirstOrDefaultAsync(x => x.Id == id);
        if (colour == null) return NotFound();

        var updateColourVM = new UpdateColourVM()
        {
            Id = colour.Id,
            Name = colour.Name,
        };
        return View(updateColourVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateColourVM updateColourVM)
    {
        var result = await _colourService.UpdateAsync(updateColourVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
