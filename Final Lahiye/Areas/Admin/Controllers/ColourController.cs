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

    public ColourController(AppDbContext context,ILogger<ColourController> logger)
    {
        _context = context;
        _logger = logger;
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

        Colour colour = new()
        {
            Name = createColourVM.Name,
        };
        await _context.Colours.AddAsync(colour);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var colour = await _context.Colours.FirstOrDefaultAsync(x => x.Id == id);
        if (colour == null)
            return NotFound();
        return View(colour);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var colour = await _context.Colours.FirstOrDefaultAsync(x => x.Id == id);
        if (colour == null)
            return View();

        _context.Colours.Remove(colour);
        await _context.SaveChangesAsync();
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

        var colour = await _context.Colours.FirstOrDefaultAsync(x => x.Id == updateColourVM.Id);
        if (colour == null) return NotFound();

        colour.Name = updateColourVM.Name;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
